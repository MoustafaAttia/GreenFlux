using GreenFluxAPI.Model;
using GreenFluxAPI.Service;
using GreenFluxAPI.Utilities;
using System;
using System.Collections.Generic;

namespace GreenFluxAPI.BusinessLogic
{
    public class PublicHolidaysBL : IPublicHolidaysBL
    {
        #region Declare Variables
        private IPublicHolidaysService _publicHolidaysService;
        #endregion

        #region Constructor
        public PublicHolidaysBL(IPublicHolidaysService publicHolidaysService)
        {
            this._publicHolidaysService = publicHolidaysService;
        }
        #endregion

        #region Methods
        public CountryPublicHoliday GetMaxHolidaysCountry(int year)
        {
            var allHolidays = this._publicHolidaysService.GetPublicHolidaysByYear(year);
            if (allHolidays != null && allHolidays.Result != null && allHolidays.Result.Status == true)
            {
                CountryPublicHoliday result = new CountryPublicHoliday();
                int maxHolidays = 0;
                foreach (var country in allHolidays.Result.CountryPublicHolidays)
                {
                    if (country.PublicHolidays.Count > maxHolidays)
                    {
                        result = country;
                        maxHolidays = country.PublicHolidays.Count;
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public HolidaysMonth GetMaxHolidaysMonthGlobally(int year)
        {
            var allHolidays = this._publicHolidaysService.GetPublicHolidaysByYear(year);
            if (allHolidays != null && allHolidays.Result != null && allHolidays.Result.Status == true)
            {
                HolidaysMonth result = new HolidaysMonth();
                int maxMonthCount = 0;
                List<HolidaysMonth> monthsCount = GetMonthsCount();

                foreach (CountryPublicHoliday country in allHolidays.Result.CountryPublicHolidays)
                {
                    foreach (PublicHoliday holiday in country.PublicHolidays)
                    {
                        if (holiday.Global)
                        {
                            if (!monthsCount[holiday.Date.Month-1].Holidays.Contains(holiday.Date.Day))
                            {
                                monthsCount[holiday.Date.Month-1].Holidays.Add(holiday.Date.Day);
                            }
                        }
                    }
                }

                foreach (var item in monthsCount)
                {
                    if (item.Holidays.Count > maxMonthCount)
                    {
                        result.MonthName = item.MonthName;
                        result.Holidays = item.Holidays;
                        maxMonthCount = item.Holidays.Count;
                    }
                }
                return result;
            }
            else
            {
                return null;
            }
        }
        
        public CountryPublicHoliday GetMaxUniqueHolidaysCountry(int year)
        {
            // In order to get unique holidays, I will create a Dictionary of dates, and for each date will add countries that had this date as holiday
            // after that, the result will be exist in this list
            // Dictionary is better choice that list, as looking for item in Dictionary is O(1) time complexity, and in list O(n). so Dictionary will save more running time

            var allHolidays = this._publicHolidaysService.GetPublicHolidaysByYear(year);
            if (allHolidays != null && allHolidays.Result != null && allHolidays.Result.Status == true)
            {
                
                CountryPublicHoliday result = new CountryPublicHoliday();
                Dictionary<DateTime, List<PublicHoliday>> holidayDates = new Dictionary<DateTime, List<PublicHoliday>>();
                Dictionary<string, List<PublicHoliday>> countriesCount = new Dictionary<string, List<PublicHoliday>>(); // key: country name, value: all unique holidays
                int maxUnique = 0;

                foreach (CountryPublicHoliday country in allHolidays.Result.CountryPublicHolidays)
                {
                    foreach (PublicHoliday holiday in country.PublicHolidays)
                    {
                        if (!holidayDates.ContainsKey(holiday.Date))
                        {
                            holidayDates[holiday.Date] = new List<PublicHoliday>();
                            holidayDates[holiday.Date].Add(holiday);
                        }
                        else
                        {
                            holidayDates[holiday.Date].Add(holiday);
                        }
                    }
                }

                foreach (var date in holidayDates)
                {
                    if (date.Value.Count == 1)
                    {
                        if (!countriesCount.ContainsKey(date.Value[0].CountryCode))
                        {
                            countriesCount[date.Value[0].CountryCode] = new List<PublicHoliday>();
                            countriesCount[date.Value[0].CountryCode].Add(date.Value[0]);
                        }
                        else
                        {
                            countriesCount[date.Value[0].CountryCode].Add(date.Value[0]);
                        }
                    }
                }

                foreach (var country in countriesCount)
                {
                    if (country.Value.Count > maxUnique)
                    {
                        maxUnique = country.Value.Count;
                        result.CountryName = Helper.GetCountryByCode(country.Key);
                        result.CountryCode = country.Key;
                        result.UniqueHolidays = country.Value;
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public List<PublicHoliday> GetCountryPublicHoliday(int year, string countryCode)
        {
            var holidays = this._publicHolidaysService.GetPublicHolidays(year, countryCode);
            if (holidays != null && holidays.Result.Status == true)
                return holidays.Result.PublicHolidays;
            else
                return null;
        }

        public List<CountryPublicHoliday> GetCountryPublicHolidayByYear(int year)
        {
            var holidays = this._publicHolidaysService.GetPublicHolidaysByYear(year);
            if (holidays != null && holidays.Result.Status == true)
                return holidays.Result.CountryPublicHolidays;
            else
                return null;
        }
        #endregion

        #region Helper Functions
        private List<HolidaysMonth> GetMonthsCount()
        {
            return new List<HolidaysMonth>()
                {
                    new HolidaysMonth() { MonthName = "January", Holidays = new List<int>() },
                    new HolidaysMonth() { MonthName = "February", Holidays = new List<int>() },
                    new HolidaysMonth() { MonthName = "March", Holidays = new List<int>() },
                    new HolidaysMonth() { MonthName = "April", Holidays = new List<int>() },
                    new HolidaysMonth() { MonthName = "May", Holidays = new List<int>() },
                    new HolidaysMonth() { MonthName = "June", Holidays = new List<int>() },
                    new HolidaysMonth() { MonthName = "July", Holidays = new List<int>() },
                    new HolidaysMonth() { MonthName = "August", Holidays = new List<int>() },
                    new HolidaysMonth() { MonthName = "September", Holidays = new List<int>() },
                    new HolidaysMonth() { MonthName = "October", Holidays = new List<int>() },
                    new HolidaysMonth() { MonthName = "November", Holidays = new List<int>() },
                    new HolidaysMonth() { MonthName = "December", Holidays = new List<int>() }
                };
        }
        #endregion
    }
}
