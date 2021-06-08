db.createUser(
{
    user: "secret_user",
    pwd: "secret_password",
    roles: [
        {
            role: "readWrite",
            db: "short_urls"
        }
    ]

});