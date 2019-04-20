using GreenFluxAPI.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenFluxAPITest
{
    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void GetCountryByCode_WithValidParameter_ReturnExpectedResult()
        {
            // Arrange
            string countryCode = "ca";
            string expectedResult = "Canada";

            // Act
            string result = Helper.GetCountryByCode(countryCode);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        public void IsCountryCodeValid_WithValidParameter_ReturnTrue()
        {
            // Arrange
            string countryCode = "ca";

            // Act
            bool result = Helper.IsCountryCodeValid(countryCode);

            // Assert
            Assert.IsTrue(result);
        }

        public void IsCountryCodeValid_WithInvalidParameter_ReturnFalse()
        {
            // Arrange
            string countryCode = "canada";

            // Act
            bool result = Helper.IsCountryCodeValid(countryCode);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
