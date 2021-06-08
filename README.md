# Hello Shortly
URL shortening service, a web service that provides short aliases for redirection of long URLs.

# The Problem: 
Design and implement a URL Shortening Service. You may use the programming language of your choosing. Your complete solution should include a diagram outlining how different components of the system interact. It should also include a docker-compose.yml file that can be used to test your solution. The service itself must expose HTTP endpoints to carry out its expected operations. You should not include a front end to your solution. The API and supporting infrastructure is what will be reviewed.
This service must be capable of handling immense scale for both creation of new shortened URLs, as well as performing the redirects themselves. You may use any technology available to you in order to carry out this task; but, the service itself must be written by you. You can assume that a shortened URL is immutable.

# Solutions:

There are several sub problems inside of that above described problem. The challenges are:

1. We need to use unique set of characters as aliases to keep each short url unique. For example: For a long url if the short url is http://t.co/abf then "abf" represents the aliases of the url. 
2. Url's aliases size should be maintain as low as possible.
3. Have to find a way to generate and distribute the unique alieases fast.
4. Site traffic would be very high.

**Solution for 1 and 2:**
This problem could be easily solve by generating 62 base numbers. We have 26 lowercase letter[a-z], 26 uppercase letters[A-Z] and 10 numbers[0-9]. If we could represent 62 (26+26+10) base number by using them then for length 7, we could generate 62^7 = 3521614606208 numbers of unique combinations. If our system could convert
200 long urls to short urls per second then it will take more than 500 years to reach length 8. So I think it's one of the best way to keep the url's aliases short as well as unique.

**Solution for number 3:**

Our system would be read heavy, means there would be a lot people use our site for converting long url to short url instead of generating short from a long one, per second. I keep that thing in my mind while designing the solutions. 

I divided the whole solutions in 2 parts and created two microservices for that. They are:

1. key-range-provider service
2. url_distributer service

There responsibilities are very simple. let's discuss about that.

1. Key Range provider service: It's a rest api. It has only one GET endpoint. If any consumer request that endpoint it provides a range of two numbers. For example: let's have a look on the service's GET response,

{
    "range_id": 29,
    "start_range": 9000001,
    "end_range": 10000000
}


- start_range = range start point
- end_range = range end point

The difference between end_range and start_range is 1 Million.

This service never duplicates the same range, that means it never gonna provide response with same range to multiple consumers. That's the beauty of this service. 

Service-1 High Level Architecture: http://localhost:4000

![service_1_high_level_architecture](https://user-images.githubusercontent.com/5144847/121199312-adcb6580-c827-11eb-8fe9-980e24cdd1e5.png)






2. **Url distribution service:** 

This service has only url distribution responsibility, either it could be after converting short to long or long to short url. let's talk about long to short url conversion process first, this process represents about write data. 

The Url distribution service has a background service that pulls key ranges from "Key Range Provider Service" and store that in a concurrent queue and check every 5 sec that if the queue has enough items or not. While monitoring if it's find that, the queue is empty then the background service pulls another set of key range and enque the concurrent queue. When user comes to convert a long url to short that time rest api safely deque an item from the concurrent queue and generates an unique url and provides to user. The concurrent queue ensure that every long to short url request on server gets the unique value for url aliases. 

Now let talk about read data or short to long conversion process. 













