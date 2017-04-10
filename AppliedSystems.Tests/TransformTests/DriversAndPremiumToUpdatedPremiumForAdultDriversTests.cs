using System;
using AppliedSystems.Dtos;
using AppliedSystems.Dtos.Stubs;
using AppliedSystems.GenericStubs;
using AppliedSystems.Interfaces;
using AppliedSystems.Transforms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppliedSystems.Tests.TransformTests
{
    [TestClass]
    public class DriversAndPremiumToUpdatedPremiumForAdultDriversTests
    {
                [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ObjectCreationAsStatement
            new DriversAndPremiumToUpdatedPremiumForAdultDrivers(21, 26, null, 0.2);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_argument()
        {
            var driversAndPremiumToUpdatedPremiumForYoungDrivers = new DriversAndPremiumToUpdatedPremiumForAdultDrivers(21, 26, new GenericStubProvider<DateTime>(), 0.2 );

            // ReSharper disable once AssignNullToNotNullAttribute
            driversAndPremiumToUpdatedPremiumForYoungDrivers.Transform(null);
        }

        [TestMethod]
        public void Should_return_original_premium_when_driver_is_younger_than_26()
        {
            const double increaseAmount = 0.1;
            const int startingAdultAge = 26;
            const int endingAdultAge = 75;
            const string driverName = "name";
            const string occupation = "occupation";
            const double expected = 500.00;
            var todayProvider = new GenericStubProvider<DateTime>(new[]{ DateTime.Today, DateTime.Today });
            var driverDateOfBirth = DateTime.Now.AddYears(-25);
            var driverClaims = new IClaim[0];
            var driversAndPremiumToUpdatedPremiumForYoungDrivers = new DriversAndPremiumToUpdatedPremiumForAdultDrivers(startingAdultAge, endingAdultAge, todayProvider, increaseAmount );
            var driver = new DriverStub(driverName, occupation, driverDateOfBirth, driverClaims );
            var driversAndPremium = new DriverAndPremium(driver, expected);

            var actual = driversAndPremiumToUpdatedPremiumForYoungDrivers.Transform(driversAndPremium);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_decrease_original_premium_by_10_percent_when_a_driver_is_older_26()
        {
            const double expected = 450.00;
            const double increaseAmount = 0.1;
            const int startingAdultAge = 26;
            const int endingAdultAge = 75;
            const string firstDriverName = "name";
            const string occupation = "occupation";
            const double premium = 500.00;
            var todayProvider = new GenericStubProvider<DateTime>(new[]{ DateTime.Today, DateTime.Today });
            var firstDriverDateOfBirth = DateTime.Now.AddYears(-27);
            var firstDriverClaims = new IClaim[0];
            var driversAndPremiumToUpdatedPremiumForYoungDrivers = new DriversAndPremiumToUpdatedPremiumForAdultDrivers(startingAdultAge, endingAdultAge, todayProvider, increaseAmount );
            var driver = new DriverStub(firstDriverName, occupation, firstDriverDateOfBirth, firstDriverClaims);
            var driversAndPremium = new DriverAndPremium(driver, premium);

            var actual = driversAndPremiumToUpdatedPremiumForYoungDrivers.Transform(driversAndPremium);

            Assert.AreEqual(expected, actual);
        }
    }
}
