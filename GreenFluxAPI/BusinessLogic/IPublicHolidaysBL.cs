using GreenFluxAPI.Model;
using System.Collections.Generic;

namespace GreenFluxAPI.BusinessLogic
{
    public interface IPublicHolidaysBL
    {
        CountryPublicHoliday GetMaxHolidaysCountry(int year);

        HolidaysMonth GetMaxHolidaysMonthGlobally(int year);

        CountryPublicHoliday GetMaxUniqueHolidaysCountry(int year);

        List<PublicHoliday> GetCountryPublicHoliday(int year, string countryCode);

        List<CountryPublicHoliday> GetCountryPublicHolidayByYear(int year);
    }
}
