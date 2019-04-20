using GreenFluxAPI.ServiceObjects;
using System.Threading.Tasks;

namespace GreenFluxAPI.Service
{
    public interface IPublicHolidaysService
    {
        Task<PublicHolidayResponse> GetPublicHolidays(int year, string countryCode);
        Task<CountryPublicHolidayResponse> GetPublicHolidaysByYear(int year);
    }
}
