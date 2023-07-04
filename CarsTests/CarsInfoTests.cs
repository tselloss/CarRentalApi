using Cars.MockHelper;
using Cars.Repository;
using FluentAssertions;
using NUnit.Framework;

namespace CarsTests
{
    public class CarsInfoTests
    {
        private MockHelperCars cars;
        [SetUp]
        public void SetUp()
        {
            cars = new MockHelperCars();
        }

        [Test]
        public void GetAllCars_ReturnListOfCars()
        {
            // Arrange
            var expectedCarsList = cars.GetCarsList();
            var carsService = new CarsService();

            // Act
            var actualCarList = carsService.GetAllCars();

            // Assert
            actualCarList.Should().BeEquivalentTo(expectedCarsList);
        }
    }
}
