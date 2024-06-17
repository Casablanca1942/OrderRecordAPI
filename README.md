# OrderRecordAPI

## Tech stack:
1. Azure SQL
2. ASP.NET Minimal API (EF Core)

So, after a period of hardworking, it works! I added the connection string as the environment variable in Azure App Service.

\n
You can test the API methods [here](orderrecordapi20240612.azurewebsites.net/Swagger).

_Please note that the first request will always time out with error code 500 as I chose free App service plan (which is cold-start)._

The future React Frontend will interact with:
> orderrecordapi20240612.azurewebsites.net/OrderRecord


GitHub Actions is configured for CI/CD.
