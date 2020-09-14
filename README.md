# redis-cache
Sample program exploring using redis as a distributed caching system.

## Quick start

### Prerequisites
* Docker 
* Dot Net Core


**Running the application**

open file `src\Api\appsettings.json` and add your own RapidApi key
```json
"RapidApi": {
    "baseUrl": "https://matchilling-chuck-norris-jokes-v1.p.rapidapi.com",
    "host": "matchilling-chuck-norris-jokes-v1.p.rapidapi.com",
    "key": "<supply key here>"
  },
```
Then run the following commands:
```
$ docker-compose build
$ docker-compose up
```

**Building the application**
```
$ dotnet build redis-cache.sln
```