# OrderRecordAPI

## Tech stack:
1. Azure SQL
2. ASP.NET Minimal API (EF Core)

![image](https://github.com/Casablanca1942/OrderRecordAPI/assets/109644322/1b287aa5-bfbb-41df-9a94-dcd0e6913882)

## Link

You can test the API methods [here](orderrecordapi20240612.azurewebsites.net/Swagger).

_Please note that **the first request will always time out with error code 500** as I chose free App service plan (which is cold-start)._

## Note
1. The future React Frontend will interact with:
  > orderrecordapi20240612.azurewebsites.net/OrderRecord
2. GitHub Actions is configured for CI/CD.
3. I added the connection string to Azure SQL as the environment variable in Azure App Service.
