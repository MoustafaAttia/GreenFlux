using System;
using GreenFluxAPI.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using GreenFluxAPI.Utilities;
using GreenFluxAPI.ServiceObjects;

namespace GreenFluxAPI.Service
{
    public class PublicHolidaysService : IPublicHolidaysService
    {
        #region Declare Variables
        private const string PublicHolidaysAPIUrl = "https://date.nager.at/api/v2/PublicHolidays/{0}/{1}";
        private const string DateTimeFormat = "yyyy-MM-dd";
        private DataContractJsonSerializerSettings serializerSettings;
        private PublicHolidayResponse publicHolidayResponse;
        private CountryPublicHolidayResponse countryPublicHolidayResponse;
        private DataContractJsonSerializer serializer;
        #endregion

        #region Service Methods
        public async Task<PublicHolidayResponse> GetPublicHolidays(int year, string countryCode)
        {
            if (year < 0 || !Helper.IsCountryCodeValid(countryCode.ToUpper()))
            {
                this.publicHolidayResponse = new PublicHolidayResponse();
                this.publicHolidayResponse.PublicHolidays = null;
                this.publicHolidayResponse.Status = false;
                this.publicHolidayResponse.ExceptionMessage = "Invalid Year or Country code.";
                return this.publicHolidayResponse;
            }
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    this.serializerSettings = new DataContractJsonSerializerSettings();
                    this.serializerSettings.DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat(DateTimeFormat);
                    serializer = new DataContractJsonSerializer(typeof(List<PublicHoliday>), serializerSettings);
                    var stream = client.GetStreamAsync(string.Format(PublicHolidaysAPIUrl, year, countryCode));
                    var response = serializer.ReadObject(await stream) as List<PublicHoliday>;
                    if (response != null)
                    {
                        this.publicHolidayResponse = new PublicHolidayResponse();
                        this.publicHolidayResponse.PublicHolidays = response;
                        this.publicHolidayResponse.Status = true;
                        this.publicHolidayResponse.ExceptionMessage = string.Empty;
                        return this.publicHolidayResponse;
                    }
                    else
                    {
                        this.publicHolidayResponse = new PublicHolidayResponse();
                        this.publicHolidayResponse.PublicHolidays = null;
                        this.publicHolidayResponse.Status = false;
                        this.publicHolidayResponse.ExceptionMessage = "An unexpected Exception happend, Please try again.";
                        return this.publicHolidayResponse;
                    }
                    
                }
                catch (Exception ex)
                {
                    this.publicHolidayResponse = new PublicHolidayResponse();
                    this.publicHolidayResponse.PublicHolidays = null;
                    this.publicHolidayResponse.Status = false;
                    this.publicHolidayResponse.ExceptionMessage = ex.Message;
                    return this.publicHolidayResponse;
                }
                
            }
        }

        public async Task<CountryPublicHolidayResponse> GetPublicHolidaysByYear(int year)
        {
            if (year < 0)
            {
                this.countryPublicHolidayResponse = new CountryPublicHolidayResponse();
                this.countryPublicHolidayResponse.CountryPublicHolidays = null;
                this.countryPublicHolidayResponse.Status = false;
                this.countryPublicHolidayResponse.ExceptionMessage = "Invalid Year.";
                return this.countryPublicHolidayResponse;
            }
            using (HttpClient client = new HttpClient())
            {
                string[] countriesCodes = Helper.GetCountriesCodes();
                this.countryPublicHolidayResponse = new CountryPublicHolidayResponse();
                this.countryPublicHolidayResponse.CountryPublicHolidays = new List<CountryPublicHoliday>();
                CountryPublicHoliday currCountryPublicHoliday;
                
                foreach (string code in countriesCodes)
                {
                    var publicHolidaysResponse = await GetPublicHolidays(year, code);
                    currCountryPublicHoliday = new CountryPublicHoliday();
                    currCountryPublicHoliday.PublicHolidays = publicHolidaysResponse.PublicHolidays;
                    currCountryPublicHoliday.CountryName = Helper.GetCountryByCode(code);
                    currCountryPublicHoliday.CountryCode = code;
                    this.countryPublicHolidayResponse.CountryPublicHolidays.Add(currCountryPublicHoliday);
                }

                this.countryPublicHolidayResponse.Status = true;
                return this.countryPublicHolidayResponse;
            }
        }
        #endregion
    }
}
