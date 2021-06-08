# Hello Shortly
URL shortening service, a web service that provides short aliases for redirection of long URLs.

# The Problem: 
Design and implement a URL Shortening Service. You may use the programming language of your choosing. Your complete solution should include a diagram outlining how different components of the system interact. It should also include a docker-compose.yml file that can be used to test your solution. The service itself must expose HTTP endpoints to carry out its expected operations. You should not include a front end to your solution. The API and supporting infrastructure is what will be reviewed.
This service must be capable of handling immense scale for both creation of new shortened URLs, as well as performing the redirects themselves. You may use any technology available to you in order to carry out this task; but, the service itself must be written by you. You can assume that a shortened URL is immutable.

# Solutions:

There are several sub problems inside of the above described problem. The challenges are:

1. We need to use unique set of characters as aliases to keep each short URL unique. For example, for a long URL, if the short URL is http://t.co/abf then "abf" represents the alias of the URL. 
2. URL's aliases size should be as low as possible.
3. Have to find a way to generate and distribute the unique aliases fast.
4. Site traffic will be very high.

**Solution for 1 and 2:**
These problems could be easily solved by generating 62 base numbers. We have 26 lowercase letter[a-z], 26 uppercase letters[A-Z], and 10 numbers[0-9]. If we could represent 62 (26+26+10) base number by using the letters and numbers, then for length 7, we could generate 62^7 = 3521614606208 numbers of unique combinations. If our system could convert 200 long URLs to short URLs per second, then it will take more than 500 years to reach length 8. So, I think this is one of the best ways to keep the URL's aliases short as well as unique.

**Solution for 3:**
Our system will be read heavy, which means a lot of users will be using our site for converting short URL to long URL (read) instead of generating short from a long one (write) per second. I kept this in my mind while designing the solution. 

I divided the whole solution into two parts and created two microservices. They are:

1. key-range-provider service
2. url_distributor service

Their responsibilities are very simple. Let's discuss about that.

1. Key range provider service: This is a rest api. It has only one GET endpoint. If any consumer requests through that endpoint, it provides a range of two numbers. For example, let's have a look on a sample GET json response of the service,

{
    "range_id": 29,
    "start_range": 9000001,
    "end_range": 10000000
}

where, start_range = range start point
and end_range = range end point

The difference between end_range and start_range is 1 million.

This service never duplicates a range, that means it will never provide response with the same range to multiple consumers. That is the beauty of this service. 

High Level Architecture for Service-1: http://localhost:4000

![service_1_high_level_architecture](https://user-images.githubusercontent.com/5144847/121199312-adcb6580-c827-11eb-8fe9-980e24cdd1e5.png)






2. **URL distributor service:** 

This service has only one responsibility, URL distribution. It could either be after converting short to long or long to short URL. Let's talk about the long to short URL conversion process first. This process represents writing data.

The URL distribution service has a background service that pulls key ranges from the "Key Range Provider Service" and stores that in a concurrent queue. It checks the queue every 5 sec to see if it has enough items or not. While monitoring, if it finds that the queue is empty, then the background service pulls another set of key range and enques the concurrent queue. When a user comes to convert a long URL to short, that time the rest api safely deques an item from the concurrent queue, generates a unique URL, and provides it to the user. The concurrent queue ensures that every long to short URL request on the server gets a unique value for the URL aliases. The service stores the data to a MongoDB data storage as well as a Redis cache server, so that the read operations could find and serve the data faster.

Now, let's talk about the read data or short to long conversion process. In this process, a user provides our server generated short URL and expects the original long URL. After receiving the short URL from the user, the service first searches the cache for the long URL. If found, then it returns the long URL from there. Otherwise, it searches the MongoDB storage and returns if found. If not found, then the service declares that the short aliase was not produced by our system.


High Level Architecture for Service-2: http://localhost:5000

![service_2_HLA](https://user-images.githubusercontent.com/5144847/121208879-41546480-c82f-11eb-8736-05db6054dd44.png)












