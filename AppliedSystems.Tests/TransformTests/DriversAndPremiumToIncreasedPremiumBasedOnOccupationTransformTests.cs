using System;
using AppliedSystems.Dtos;
using AppliedSystems.Dtos.Stubs;
using AppliedSystems.Interfaces;
using AppliedSystems.Transforms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppliedSystems.Tests.TransformTests
{
    [TestClass]
    public class DriversAndPremiumToIncreasedPremiumBasedOnOccupationTransformTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument_for_jobWherePremiumIsIncreased()
        {
            const double increaseAmount = 0.1;
            // ReSharper disable once ObjectCreationAsStatement
            // ReSharper disable once AssignNullToNotNullAttribute
            new DriversAndPremiumToIncreasedPremiumBasedOnOccupationTransform(null, increaseAmount);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_argument()
        {
            const double increaseAmount = 0.1;
            var jobsWherePremiumIsIncreased = new [] { "Chauffeur"};
            var driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform =  new DriversAndPremiumToIncreasedPremiumBasedOnOccupationTransform(jobsWherePremiumIsIncreased, increaseAmount);

            // ReSharper disable once AssignNullToNotNullAttribute
            driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform.Transform(null);
        }

        [TestMethod]
        public void Should_return_original_premium_when_neither_driver_is_a_chauffeur()
        {
            const double increaseAmount = 0.1;
            const double expected = 500.00;
            const string driversName = "name";
            const string driversOccupation = "Mechanic";
            var driversDateOfBirth = DateTime.Now;
            var driversClaims = new IClaim[0];
            var jobsWherePremiumIsIncreased = new [] { "Chauffeur"};
            var driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform =  new DriversAndPremiumToIncreasedPremiumBasedOnOccupationTransform(jobsWherePremiumIsIncreased, increaseAmount);
            var driver = new DriverStub(driversName, driversOccupation, driversDateOfBirth, driversClaims);
            var driverAndPremium = new DriverAndPremium( driver, expected);

            var actual = driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform.Transform(driverAndPremium);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_return_increase_premium_when_driver_is_a_chauffeur()
        {
            const double increaseAmount = 0.1;
            const double expected = 550.00;
            const string driversName = "name";
            const string driversOccupation = "Chauffeur";
            const double premium = 500.00;
            var driversDateOfBirth = DateTime.Now;
            var driversClaims = new IClaim[0];
            var jobsWherePremiumIsIncreased = new [] { "Chauffeur"};
            var driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform =  new DriversAndPremiumToIncreasedPremiumBasedOnOccupationTransform(jobsWherePremiumIsIncreased, increaseAmount);
            var driver = new DriverStub(driversName, driversOccupation, driversDateOfBirth, driversClaims);
            var driverAndPremium = new DriverAndPremium(driver, premium);

            var actual = driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform.Transform(driverAndPremium);

            Assert.AreEqual(expected, actual);
        }
    }
}
