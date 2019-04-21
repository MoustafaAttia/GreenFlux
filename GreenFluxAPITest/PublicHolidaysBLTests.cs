using GreenFluxAPI.BusinessLogic;
using GreenFluxAPI.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFluxAPITest
{
    [TestClass]
    public class PublicHolidaysBLTests
    {
        [TestMethod]
        public void GetCountryPublicHoliday_WithValidParameters_ReturnExpectedResult()
        {
            // Arrange
            IPublicHolidaysService publicHolidaysService = new PublicHolidaysService();
            PublicHolidaysBL publicHolidaysBL = new PublicHolidaysBL(publicHolidaysService);

            // Act
            var result = publicHolidaysBL.GetCountryPublicHoliday(2019, "nl");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 10);
        }

        [TestMethod]
        public void GetCountryPublicHoliday_WithInvalidParameters_ReturnNull()
        {
            // Arrange
            IPublicHolidaysService publicHolidaysService = new PublicHolidaysService();
            PublicHolidaysBL publicHolidaysBL = new PublicHolidaysBL(publicHolidaysService);

            // Act
            var result = publicHolidaysBL.GetCountryPublicHoliday(2019, "aaa");

            // Assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public void GetCountryPublicHolidayByYear_WithValidParameters_ReturnExpectedResult()
        {
            // Arrange
            IPublicHolidaysService publicHolidaysService = new PublicHolidaysService();
            PublicHolidaysBL publicHolidaysBL = new PublicHolidaysBL(publicHolidaysService);

            // Act
            var result = publicHolidaysBL.GetCountryPublicHolidayByYear(2019);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 92);
        }

        [TestMethod]
        public void GetCountryPublicHolidayByYear_WithInvalidParameters_ReturnNull()
        {
            // Arrange
            IPublicHolidaysService publicHolidaysService = new PublicHolidaysService();
            PublicHolidaysBL publicHolidaysBL = new PublicHolidaysBL(publicHolidaysService);

            // Act
            var result = publicHolidaysBL.GetCountryPublicHolidayByYear(-2019);

            // Assert
            Assert.IsNull(result);
        }
    }
}
