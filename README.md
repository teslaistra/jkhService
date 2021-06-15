# jkhService
## Base of service which will help you to check the state of your house and make complaints for locals. 

#### Consists from:
1. Gateway. It will redirect all requests to exact microservies. 
2. Topics Service. This service is a base for administration database wia API. It can add, change, delete and return a list of topics. This list of topics needed for checking your house.
3. Complaints service. This service is a key service. It can receive a base information about the complaint(date, address, user who created and municipal deputy who is assigned to help for this complaint. Also this service realizes geocoding which is needed for making a map of complaints later on the frontend. Geocoding is provided by Google Maps Api. 

## Other features:
1. All services have support for logging and sending errors to Sentry.
2. MySQL database is hosted on third party server.
3. Service need a google api key to make geocoding 
4. appsettings with connection strings and api keys are removed because of privacy reasons. 

Created for LPO course in NUST "MISIS" at spring semester 2021.
