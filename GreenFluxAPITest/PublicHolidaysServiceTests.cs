using GreenFluxAPI.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFluxAPITest
{
    [TestClass]
    public class PublicHolidaysServiceTests
    {
        [TestMethod]
        public void GetPublicHolidays_WithValidParameters_ReturnExpectedResult()
        {
            // Arrange
            PublicHolidaysService publicHolidaysService = new PublicHolidaysService();

            // Act
            var result = publicHolidaysService.GetPublicHolidays(2019, "nl");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsTrue(result.Result.Status);
            Assert.IsNotNull(result.Result.PublicHolidays);
            Assert.AreEqual(result.Result.PublicHolidays.Count, 10);
        }

        [TestMethod]
        public void GetPublicHolidays_WithInvalidParameters_ReturnExceptionResult()
        {
            // Arrange
            PublicHolidaysService publicHolidaysService = new PublicHolidaysService();

            // Act
            var result = publicHolidaysService.GetPublicHolidays(2019, "aaa");


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsFalse(result.Result.Status);
            Assert.IsNull(result.Result.PublicHolidays);
            Assert.IsNotNull(result.Result.ExceptionMessage);
        }

        [TestMethod]
        public void GetPublicHolidaysByYear_WithValidParameters_ReturnExpectedResult()
        {
            // Arrange
            PublicHolidaysService publicHolidaysService = new PublicHolidaysService();

            // Act
            var result = publicHolidaysService.GetPublicHolidaysByYear(2019);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsTrue(result.Result.Status);
            Assert.IsNotNull(result.Result.CountryPublicHolidays);
            Assert.AreEqual(result.Result.CountryPublicHolidays.Count, 92);
        }

        [TestMethod]
        public void GetPublicHolidaysByYear_WithInvalidParameters_ReturnExceptionResult()
        {
            // Arrange
            PublicHolidaysService publicHolidaysService = new PublicHolidaysService();

            // Act
            var result = publicHolidaysService.GetPublicHolidaysByYear(-2019);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsFalse(result.Result.Status);
            Assert.IsNull(result.Result.CountryPublicHolidays);
            Assert.IsNotNull(result.Result.ExceptionMessage);
        }
    }
}
