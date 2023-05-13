This is a DB first api that can communicate with a database that fits the models and Filmsystemcontext files.

To use it you need the db and when you have it you need to add the right connectionstring to appsettings.json, when that is done you can run the application and use the different API endpoints to retrive or create data for the database from the swagger page.

This db is also connected to TMDB to retrive movie suggestions, allthough that can be hard to read for now, and i might update it to clean that up. You also need to use the IDs that tmdb is using which you cna only find out with googling them.