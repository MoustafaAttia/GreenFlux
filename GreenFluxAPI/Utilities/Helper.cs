using System.Collections.Generic;

namespace GreenFluxAPI.Utilities
{
    public static class Helper
    {
        private static Dictionary<string, string> countries;

        static Helper()
        {
            countries = InitializeCountriesCodes();
        }

        public static bool IsCountryCodeValid(string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode)) return false;
            return countries.ContainsKey(countryCode.ToUpper());
        }

        public static string[] GetCountriesCodes()
        {
            return new string[]
            {
                "AD", "AR", "AT", "AU", "AX",
                "BB", "BE", "BG", "BO", "BR", "BS", "BW", "BY", "BZ", "CA", "CH", "CL", "CN", "CO", "CR", "CU", "CY",
                "CZ", "DE", "DK", "DO", "EC", "EE", "EG", "ES", "FI", "FO", "FR", "GA", "GB", "GD", "GL", "GR", "GT",
                "GY", "HN", "HR", "HT", "HU", "IE", "IM", "IS", "IT", "JE", "JM", "LI", "LS", "LT", "LU", "LV", "MA",
                "MC", "MD", "MG", "MK", "MT", "MX", "MZ", "NA", "NI", "NL", "NO", "NZ", "PA", "PE", "PL", "PR",
                "PT", "PY", "RO", "RS", "RU", "SE", "SI", "SJ", "SK", "SM", "SR", "SV", "TN", "TR", "UA", "US", "UY",
                "VA", "VE", "ZA"
            };
        }

        public static string GetCountryByCode(string countryCode)
        {
            if (countries != null && countries.ContainsKey(countryCode.ToUpper()))
            {
                return countries[countryCode.ToUpper()];
            }
            else
            {
                return null;
            }
        }

        private static Dictionary<string, string> InitializeCountriesCodes()
        {
            return new Dictionary<string, string>()
            {
                { "AD", "Andorra" },
                { "AR", "Argentina" },
                { "AT", "Austria" },
                { "AU", "Australia" },
                { "AX", "Åland Islands" },
                { "BB", "Barbados" },
                { "BE", "Belgium" },
                { "BG", "Bulgaria" },
                { "BO", "Bolivia" },
                { "BR", "Brazil" },
                { "BS", "Bahamas" },
                { "BW", "Botswana" },
                { "BY", "Belarus" },
                { "BZ", "Belize" },
                { "CA", "Canada" },
                { "CH", "Switzerland" },
                { "CL", "Chile" },
                { "CN", "China" },
                { "CO", "Colombia" },
                { "CR", "Costa Rica" },
                { "CU", "Cuba" },
                { "CY", "Cyprus" },
                { "CZ", "Czechia" },
                { "DE", "Germany" },
                { "DK", "Denmark" },
                { "DO", "Dominican Republic" },
                { "EC", "Ecuador" },
                { "EE", "Estonia" },
                { "EG", "Egypt" },
                { "ES", "Spain" },
                { "FI", "Finland" },
                { "FO", "Faroe Islands" },
                { "FR", "France" },
                { "GA", "Gabon" },
                { "GB", "United Kingdom of Great Britain and Northern Ireland" },
                { "GD", "Grenada" },
                { "GL", "Greenland" },
                { "GR", "Greece" },
                { "GT", "Guatemala" },
                { "GY", "Guyana" },
                { "HN", "Honduras" },
                { "HR", "Croatia" },
                { "HT", "Haiti" },
                { "HU", "Hungary" },
                { "IE", "Ireland" },
                { "IM", "Isle of Man" },
                { "IS", "Iceland" },
                { "IT", "Italy" },
                { "JE", "Jersey" },
                { "JM", "Jamaica" },
                { "LI", "Liechtenstein" },
                { "LS", "Lesotho" },
                { "LT", "Lithuania" },
                { "LU", "Luxembourg" },
                { "LV", "Latvia" },
                { "MA", "Morocco" },
                { "MC", "Monaco" },
                { "MD", "Moldova" },
                { "MG", "Madagascar" },
                { "MK", "The former Yugoslav Republic of Macedonia" },
                { "MT", "Malta" },
                { "MX", "Mexico" },
                { "MZ", "Mozambique" },
                { "NA", "Namibia" },
                { "NI", "Nicaragua" },
                { "NL", "Netherlands" },
                { "NO", "Norway" },
                { "NZ", "New Zealand" },
                { "PA", "Panama" },
                { "PE", "Peru" },
                { "PL", "Poland" },
                { "PR", "Puerto Rico" },
                { "PT", "Portugal" },
                { "PY", "Paraguay" },
                { "RO", "Romania" },
                { "RS", "Serbia" },
                { "RU", "Russian Federation" },
                { "SE", "Sweden" },
                { "SI", "Slovenia" },
                { "SJ", "Svalbard and Jan Mayen" },
                { "SK", "Slovakia" },
                { "SM", "San Marino" },
                { "SR", "Suriname" },
                { "SV", "El Salvador" },
                { "TN", "Tunisia" },
                { "TR", "Turkey" },
                { "UA", "Ukraine" },
                { "US", "United States of America" },
                { "UY", "Uruguay" },
                { "VA", "Holy See" },
                { "VE", "Bolivarian Republic of Venezuela" },
                { "ZA", "South Africa" }
            };
        }
    }
}
