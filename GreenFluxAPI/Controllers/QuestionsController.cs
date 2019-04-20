using GreenFluxAPI.BusinessLogic;
using GreenFluxAPI.Model;
using GreenFluxAPI.Service;
using GreenFluxAPI.ServiceObjects;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GreenFluxAPI.Controllers
{
    // test push github
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        #region Declare Variables
        private const string FirstQuestionString = "Which country had the most holidays this year?";
        private const string SecondQuestionString = "Which month had most holidays if you compare globally?";
        private const string ThirdQuestionString = "Which country had the most unique holidays?";
        private PublicHolidaysBL publicHolidaysOperations;
        private AnswerResponse answerResponse;
        private int throttleSeconds = 5;
        #endregion

        #region Constructor
        public QuestionsController()
        {
            this.publicHolidaysOperations = new PublicHolidaysBL();
        }
        #endregion

        #region Controller Methods
        [HttpGet]
        [Route("FirstQuestion")]
        [Throttle(Name = "FirstQuestionThrottle", Seconds = 5)]
        public AnswerResponse FirstQuestion()
        {
            CountryPublicHoliday result = this.publicHolidaysOperations.GetMaxHolidaysCountry(2019);
            if (result != null)
            {
                this.answerResponse = new AnswerResponse();
                this.answerResponse.AdditionalInfo = new List<string>();
                this.answerResponse.QuestionString = FirstQuestionString;
                this.answerResponse.Answer = result.CountryCode + ", " + result.CountryName;
                foreach (var holiday in result.PublicHolidays)
                {
                    this.answerResponse.AdditionalInfo.Add(string.Format("{0} is public holiday for {1}", holiday.Date.ToShortDateString(), holiday.Name));
                }
                this.answerResponse.Explanation = string.Format("The country contains {0} holidays in 2019!", result.PublicHolidays.Count);
                return this.answerResponse;
            }
            else
            {
                this.answerResponse = new AnswerResponse();
                this.answerResponse.Answer = string.Empty;
                this.answerResponse.QuestionString = string.Empty;
                this.answerResponse.Explanation = "Unexpected Exception happen, Please try again.";
                return this.answerResponse;
            }
        }

        [HttpGet]
        [Route("SecondQuestion")]
        [Throttle(Name = "SecondQuestionThrottle", Seconds = 5)]
        public AnswerResponse SecondQuestion()
        {
            HolidaysMonth result = this.publicHolidaysOperations.GetMaxHolidaysMonthGlobally(2019);
            this.answerResponse = new AnswerResponse();
            if (result != null)
            {
                this.answerResponse.AdditionalInfo = new List<string>();
                this.answerResponse.QuestionString = SecondQuestionString;
                this.answerResponse.Answer = result.MonthName;
                result.Holidays.Sort();
                foreach (var day in result.Holidays)
                {
                    this.answerResponse.AdditionalInfo.Add(string.Format("Day: {0} is public holiday globally", day.ToString()));
                }
                this.answerResponse.Explanation = string.Format("The month contains {0} Holidays in 2019!", result.Holidays.Count);
            }
            else
            {
                this.answerResponse.Answer = string.Empty;
                this.answerResponse.QuestionString = string.Empty;
                this.answerResponse.Explanation = "Unexpected Exception happen, Please try again.";
                
            }
            return this.answerResponse;
        }

        [HttpGet]
        [Route("ThirdQuestion")]
        [Throttle(Name = "ThirdQuestionThrottle", Seconds = 5)]
        public AnswerResponse ThirdQuestion()
        {
            CountryPublicHoliday result = this.publicHolidaysOperations.GetMaxUniqueHolidaysCountry(2019);
            if (result != null)
            {
                this.answerResponse = new AnswerResponse();
                this.answerResponse.AdditionalInfo = new List<string>();
                this.answerResponse.QuestionString = ThirdQuestionString;
                this.answerResponse.Answer = result.CountryCode + ", " +  result.CountryName;
                foreach (var holiday in result.UniqueHolidays)
                {
                    this.answerResponse.AdditionalInfo.Add(string.Format("{0} is unique holiday for {1}", holiday.Date.ToShortDateString(), holiday.Name));
                }
                this.answerResponse.Explanation = string.Format("The country contains {0} unique holidays in 2019!", result.UniqueHolidays.Count);
                return this.answerResponse;
            }
            else
            {
                this.answerResponse = new AnswerResponse();
                this.answerResponse.Answer = string.Empty;
                this.answerResponse.QuestionString = string.Empty;
                this.answerResponse.Explanation = "Unexpected Exception happen, Please try again.";
                return this.answerResponse;
            }
        }

        [HttpGet]
        [Route("GetHolidays/{year}/{countryCode}")]
        [Throttle(Name = "GetHolidaysThrottle", Seconds = 5)]
        public PublicHolidayResponse GetHolidays(string year, string countryCode)
        {
            int _year;
            if (!int.TryParse(year, out _year))
            {
                return new PublicHolidayResponse()
                {
                    ExceptionMessage = "Invalid Year, Please pass correct year.",
                    PublicHolidays = null,
                    Status = false
                };
            }
            var holidays = this.publicHolidaysOperations.GetCountryPublicHoliday(_year, countryCode);
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
        public CountryPublicHolidayResponse GetHolidaysByYear(string year)
        {
            int _year;
            if (!int.TryParse(year, out _year))
            {
                return new CountryPublicHolidayResponse()
                {
                    ExceptionMessage = "Invalid Year, Please pass correct year.",
                    CountryPublicHolidays = null,
                    Status = false
                };
            }
            var holidays = this.publicHolidaysOperations.GetCountryPublicHolidayByYear(_year);

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
    }
}