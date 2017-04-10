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
    public class DriversAndPremiumToUpdatedPremiumForYoungDriversTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ObjectCreationAsStatement
            new DriversAndPremiumToUpdatedPremiumForYoungDrivers(21, 26, null, 0.2);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_argument()
        {
            var driversAndPremiumToUpdatedPremiumForYoungDrivers = new DriversAndPremiumToUpdatedPremiumForYoungDrivers(21, 26, new GenericStubProvider<DateTime>(), 0.2 );

            // ReSharper disable once AssignNullToNotNullAttribute
            driversAndPremiumToUpdatedPremiumForYoungDrivers.Transform(null);
        }

        [TestMethod]
        public void Should_return_original_premium_when_driver_is_older_than_25()
        {
            const double increaseAmount = 0.2;
            const int startingYoungAge = 21;
            const int endingYoungAge = 25;
            const string driverName = "name";
            const string occupation = "occupation";
            const double expected = 500.00;
            var todayProvider = new GenericStubProvider<DateTime>(new[]{ DateTime.Today, DateTime.Today });
            var driverDateOfBirth = DateTime.Now.AddYears(-35);
            var driverClaims = new IClaim[0];
            var driversAndPremiumToUpdatedPremiumForYoungDrivers = new DriversAndPremiumToUpdatedPremiumForYoungDrivers(startingYoungAge, endingYoungAge, todayProvider, increaseAmount );
            var driver = new DriverStub(driverName, occupation, driverDateOfBirth, driverClaims );
            var driversAndPremium = new DriverAndPremium(driver, expected);

            var actual = driversAndPremiumToUpdatedPremiumForYoungDrivers.Transform(driversAndPremium);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_increase_original_premium_by_20_percent_when_driver_is_24()
        {
            const double expected = 600.00;
            const double increaseAmount = 0.2;
            const int startingYoungAge = 21;
            const int endingYoungAge = 25;
            const string driverName = "name";
            const string occupation = "occupation";
            const string secondDriverName = "name2";
            const double premium = 500.00;
            var todayProvider = new GenericStubProvider<DateTime>(new[]{ DateTime.Today, DateTime.Today });
            var driverDateOfBirth = DateTime.Now.AddYears(-25);
            var driversDateOfBirth = DateTime.Now.AddYears(-40);
            var driverClaims = new IClaim[0];
            var secondDriversClaims = new IClaim[0];
            var driversAndPremiumToUpdatedPremiumForYoungDrivers = new DriversAndPremiumToUpdatedPremiumForYoungDrivers(startingYoungAge, endingYoungAge, todayProvider, increaseAmount );
            var firstDriver = new DriverStub(driverName, occupation, driverDateOfBirth, driverClaims);
            var secondDriver = new DriverStub(secondDriverName, occupation, driversDateOfBirth, secondDriversClaims);
            var drivers = new []{ firstDriver, secondDriver  };
            var driversAndPremium = new DriverAndPremium(firstDriver, premium);

            var actual = driversAndPremiumToUpdatedPremiumForYoungDrivers.Transform(driversAndPremium);

            Assert.AreEqual(expected, actual);
        }
    }
}
