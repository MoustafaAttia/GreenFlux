# PublicHolidayAPI

This repository is wrapper for nagar api used to retrieve public holidays for specific country at specific year.

## Getting Started

### Prerequisites

To start build the project you will need to install Swashbuckle.AspNetCore 4.0.1
Run the following command in Visual studio Package Manager Console :

```
Install-Package Swashbuckle.AspNetCore -Version 4.0.1
```

## Solution Details and Design
This solution based on MVC design pattern, as Controller layer calls the business logic layer and retrieve object that contains service response and the model in case of success calling. 
And business logic layer calls the service and bind it's response into model objects, which should be used in view layer (currently there is no view layer, and model binded in response result object). 
### QuestionsController
Controller layer calls the business logic layer, to return model object and assign it inside response object.
* ```AnswerQuestion(int questionNumber)``` return an ```AnswerResponse``` object which contains and answer for selected question.
* ```GetHolidays(int year, string countryCode)``` return ```PublicHolidayResponse``` which contains list of Holidays for selected country at selected year.
* ```GetHolidaysByYear(int year)``` return ```CountryPublicHolidayResponse``` which contains all holidays for all countries at selected year.
* Throttle to each controller assigned to be 5 seconds for each call

### PublicHolidaysBL
Business logic layer calls the service layer directly in order to retrieve any data needed while applying any logic
* ```GetMaxHolidaysCountry(int year)``` returns the country which contains maximum number of holidays at selected year.
* ```GetMaxHolidaysMonthGlobally(int year)``` return maximum month contains globally holidays across all countries.
* ```GetMaxUniqueHolidaysCountry(int year)``` return maximum country that contains holidays that no other country had these holidays.
* ```GetCountryPublicHoliday(int year, string countryCode)``` return list of holidays for selected country at selected year.
* ```GetCountryPublicHolidayByYear(int year)``` return list of all holidays across all countries at selected year.

### PublicHolidaysService
Service layer that calls nager api directly and and return service response that contains response result binded to model object.
* ```GetPublicHolidays(int year, string countryCode)``` call nager api with ```year``` and ```countryCode``` to return holiday for selected parameters
* ```GetPublicHolidaysByYear(int year)``` call nager with with ```year``` and pass all countries codes and return list of countries with list of holidays for each one.

## Utilities
Any utilites class or methods used in application, for example: 
* ```Helper.GetCountriesCodes()``` return list of countries codes that any layer could use it.
* ```Helper.IsCountryCodeValid(string countryCode)``` return if this ```countryCode``` is valid to pass to service.

## Technologies used

* [ASP.Net core  2.1.1](https://dotnet.microsoft.com/download/dotnet-core/2.1) - .Net core framework
* [Swashbuckle.AspNetCore 4.0.1](https://www.nuget.org/packages/Swashbuckle.AspNetCore/4.0.1) - Swagger UI
* [nager api](https://date.nager.at) Public Holidays API
* MSTest .Net core
* Dependency Injection
* Swagger UI

## Future work
* Build friendly view layer with angular 2+ that uses these controllers to view holidays in calendar for selected country and year.
* Provide more business logic functions to list some insights, e.g. most and least countries in holidays, holidays by months in each country, common holidays between two countries and so on.

## Author

* **Moustafa Attia** - *Software Engineer .Net/C#* - [Moustafa Attia](https://github.com/MoustafaAttia)

## Acknowledgments

* [Request throttling in .NET Core MVC](https://www.johanbostrom.se/blog/request-throttling-in-net-core-mvc-and-api) used in ThrottleAttribute.cs
