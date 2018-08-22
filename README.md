 - To run the application as _net462_ execute `dotnet run -c Release -f net462`
 - To run the application as _netcoreapp_ execute `dotnet run -c Release -f netcoreapp2.1`

The application starts on http://localhost:5000

To create the load you need nodejs and npm. Run `npm install` and then `npm run load`. It will run the load of 3 rps on http://localhost:5000 for 5 minutes (see load.yml).

To reproduce the issue try to restart the application during the load. Under _net462_ configuration application restarts fine. Under _netcoreapp2.1_ the application hangs and does not respond.

You also need redis running on localhost:6379 for the cache.