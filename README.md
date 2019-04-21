# PublicHolidayAPI

This repository is wrapper for nagar api used to retrieve public holidays for specific country at specific year.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

To start build the project you will need to install Swashbuckle.AspNetCore 4.0.1
Run the following command in Visual studio Package Manager Console :

```
Install-Package Swashbuckle.AspNetCore -Version 4.0.1
```

## Solution Details and Design
### Controller
Controller layer calls the business logic layer, to return model object and assign it inside response object.
* ```AnswerQuestion(int questionNumber)``` return an ```AnswerResponse``` object which contains and answer for selected question.
* ```GetHolidays(int year, string countryCode)``` return ```PublicHolidayResponse``` which contains list of Holidays for selected country at selected year.
* ```GetHolidaysByYear(int year)``` return ```CountryPublicHolidayResponse``` which contains all holidays for all countries at selected year.
* Throttle to each controller assigned to be 5 seconds for each call

### BusinessLogic
Business logic layer calls the service layer directly in order to retrieve any data needed while applying any logic
* ```GetMaxHolidaysCountry(int year)``` returns the country which contains maximum number of holidays at selected year.
* ```GetMaxHolidaysMonthGlobally(int year)``` return maximum month contains globally holidays across all countries.
* ```GetCountryPublicHoliday(int year, string countryCode)``` return list of holidays for selected country at selected year.
* ```GetCountryPublicHolidayByYear(int year)``` return list of all holidays across all countries at selected year.

## Technologies used

* [ASP.Net core  2.1.1](https://dotnet.microsoft.com/download/dotnet-core/2.1) - .Net core framework
* [Swashbuckle.AspNetCore 4.0.1](https://www.nuget.org/packages/Swashbuckle.AspNetCore/4.0.1) - Swagger UI
* [nager api](https://date.nager.at) Public Holidays API
* MSTest .Net core
* Dependency Injection

## Author

* **Moustafa Attia** - *Software Engineer .Net/C#* - [Moustafa Attia](https://github.com/MoustafaAttia)

## Acknowledgments

* [Request throttling in .NET Core MVC](https://www.johanbostrom.se/blog/request-throttling-in-net-core-mvc-and-api) used in ThrottleAttribute.cs
