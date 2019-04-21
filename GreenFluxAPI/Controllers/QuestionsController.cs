using GreenFluxAPI.BusinessLogic;
using GreenFluxAPI.Model;
using GreenFluxAPI.Service;
using GreenFluxAPI.ServiceObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GreenFluxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        #region Declare Variables
        private const string FirstQuestionString = "Which country had the most holidays this year?";
        private const string SecondQuestionString = "Which month had most holidays if you compare globally?";
        private const string ThirdQuestionString = "Which country had the most unique holidays?";
        private const string ExceptionMessageString = "Unexpected Exception happen, Please try again.";
        private IPublicHolidaysBL publicHolidaysOperations;
        private AnswerResponse answerResponse;
        #endregion

        #region Constructor
        public QuestionsController(IPublicHolidaysBL _publicHolidaysOperations)
        {
            this.publicHolidaysOperations = _publicHolidaysOperations;
        }
        #endregion

        #region Controller Methods
        [HttpGet]
        [Route("AnswerQuestion")]
        [Throttle(Name = "AnswerQuestionThrottle", Seconds = 5)]
        public AnswerResponse AnswerQuestion(int questionNumber)
        {
            if (questionNumber == 1)
            {
                CountryPublicHoliday result = this.publicHolidaysOperations.GetMaxHolidaysCountry(2019);
                if (result != null)
                {
                    this.answerResponse = FormatFirstQuestionAnswerResponse(result);
                }
                else
                {
                    return GetExceptionAnswerResponse(ExceptionMessageString);
                }
            }

            else if (questionNumber == 2)
            {
                HolidaysMonth result = this.publicHolidaysOperations.GetMaxHolidaysMonthGlobally(2019);
                if (result != null)
                {
                    this.answerResponse = FormatSecondQuestionAnswerResponse(result);
                }
                else
                {
                    return GetExceptionAnswerResponse(ExceptionMessageString);
                }
            }

            else if (questionNumber == 3)
            {
                CountryPublicHoliday result = this.publicHolidaysOperations.GetMaxUniqueHolidaysCountry(2019);
                if (result != null)
                {
                    this.answerResponse = FormatThirdQuestionAnswerResponse(result);
                }
                else
                {
                    return GetExceptionAnswerResponse(ExceptionMessageString);
                }
            }

            else
            {
                return GetExceptionAnswerResponse("Question number is not provided, Please pass correct question number.");
            }

            this.answerResponse.Status = true;
            return this.answerResponse;

        }

        [HttpGet]
        [Route("GetHolidays/{year}/{countryCode}")]
        [Throttle(Name = "GetHolidaysThrottle", Seconds = 5)]
        public PublicHolidayResponse GetHolidays(int year, string countryCode)
        {
            var holidays = this.publicHolidaysOperations.GetCountryPublicHoliday(year, countryCode);
            if (holidays != null)
            {
                return new PublicHolidayResponse()
                {
                    ExceptionMessage = string.Empty,
                    PublicHolidays = holidays,
                    Status = true
                };
            }
            else
            {
                return new PublicHolidayResponse()
                {
                    ExceptionMessage = "Unexpected Exception happen, Please try again.",
                    PublicHolidays = null,
                    Status = false
            };
            }
                
        }

        [HttpGet]
        [Route("GetHolidaysByYear/{year}")]
        [Throttle(Name = "GetHolidaysByYearThrottle", Seconds = 5)]
        public CountryPublicHolidayResponse GetHolidaysByYear(int year)
        {
            var holidays = this.publicHolidaysOperations.GetCountryPublicHolidayByYear(year);

            if (holidays != null)
            {
                return new CountryPublicHolidayResponse()
                {
                    ExceptionMessage = string.Empty,
                    CountryPublicHolidays = holidays,
                    Status = true
                };
            }
            else
            {
                return new CountryPublicHolidayResponse()
                {
                    ExceptionMessage = "Unexpected Exception happen, Please try again.",
                    CountryPublicHolidays = null,
                    Status = false
                };
            }

        }

        #endregion

        #region Utilites
        private AnswerResponse FormatFirstQuestionAnswerResponse(CountryPublicHoliday result)
        {
            AnswerResponse formattedResponse = new AnswerResponse();
            formattedResponse.AdditionalInfo = new List<string>();
            formattedResponse.QuestionString = FirstQuestionString;
            formattedResponse.Answer = result.CountryCode + ", " + result.CountryName;
            foreach (var holiday in result.PublicHolidays)
            {
                formattedResponse.AdditionalInfo.Add(string.Format("{0} is public holiday for {1}", holiday.Date.ToShortDateString(), holiday.Name));
            }
            formattedResponse.Explanation = string.Format("The country contains {0} holidays in 2019!", result.PublicHolidays.Count);

            return formattedResponse;
        }
        private AnswerResponse FormatSecondQuestionAnswerResponse(HolidaysMonth result)
        {
            AnswerResponse formattedResponse = new AnswerResponse();
            formattedResponse.AdditionalInfo = new List<string>();
            formattedResponse.QuestionString = SecondQuestionString;
            formattedResponse.Answer = result.MonthName;
            result.Holidays.Sort();
            foreach (var day in result.Holidays)
            {
                formattedResponse.AdditionalInfo.Add(string.Format("Day: {0} is public holiday globally", day.ToString()));
            }
            formattedResponse.Explanation = string.Format("The month contains {0} Holidays in 2019!", result.Holidays.Count);
            return formattedResponse;
        }
        private AnswerResponse FormatThirdQuestionAnswerResponse(CountryPublicHoliday result)
        {
            AnswerResponse formattedResponse = new AnswerResponse();
            formattedResponse.AdditionalInfo = new List<string>();
            formattedResponse.QuestionString = ThirdQuestionString;
            formattedResponse.Answer = result.CountryCode + ", " + result.CountryName;
            foreach (var holiday in result.UniqueHolidays)
            {
                formattedResponse.AdditionalInfo.Add(string.Format("{0} is unique holiday for {1}", holiday.Date.ToShortDateString(), holiday.Name));
            }
            formattedResponse.Explanation = string.Format("The country contains {0} unique holidays in 2019!", result.UniqueHolidays.Count);
            return formattedResponse;
        }
        private AnswerResponse GetExceptionAnswerResponse(string exceptionMessage)
        {
            return new AnswerResponse()
            {
                Answer = string.Empty,
                QuestionString = string.Empty,
                ExceptionMessage = exceptionMessage,
                Explanation = string.Empty,
                Status = false
            };
        }


        #endregion
    }
}