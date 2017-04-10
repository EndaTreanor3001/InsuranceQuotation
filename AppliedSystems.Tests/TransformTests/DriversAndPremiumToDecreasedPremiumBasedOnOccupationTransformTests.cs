using System;
using AppliedSystems.Dtos;
using AppliedSystems.Dtos.Stubs;
using AppliedSystems.Interfaces;
using AppliedSystems.Transforms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppliedSystems.Tests.TransformTests
{
    [TestClass]
    public class DriversAndPremiumToDecreasedPremiumBasedOnOccupationTransformTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument()
        {
            // ReSharper disable once ObjectCreationAsStatement
            // ReSharper disable once AssignNullToNotNullAttribute
            new DriversAndPremiumToDecreasedPremiumBasedOnOccupationTransform(null, 0.1);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_argument()
        {
            const double decreaseAmount = 0.1;
            var jobsWherePremiumIsDecreased = new []{ "Accountant"};
            var driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform = new DriversAndPremiumToDecreasedPremiumBasedOnOccupationTransform(jobsWherePremiumIsDecreased, decreaseAmount);

            // ReSharper disable once AssignNullToNotNullAttribute
            driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform.Transform(null);
        }

        [TestMethod]
        public void Should_return_original_premium_when_driver_is_not_an_Accountant()
        {
            const double expected = 500.00;
            const double decreaseAmount = 0.1;
            const string driversName = "name";
            const string driversOccupation = "Electrician";
            var driversDateOfBirth = DateTime.Now;
            var driversClaims = new IClaim[0];
            var jobsWherePremiumIsDecreased = new []{ "Accountant"};
            var driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform = new DriversAndPremiumToDecreasedPremiumBasedOnOccupationTransform(jobsWherePremiumIsDecreased, decreaseAmount);
            var firstDriver = new DriverStub(driversName, driversOccupation, driversDateOfBirth, driversClaims);
            var driverAndPremium = new DriverAndPremium(firstDriver, expected);

            var actual = driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform.Transform(driverAndPremium);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_return_decreased_premium_when_a_driver_is_an_Accountant()
        {
            const double expected = 450.00;
            const double decreaseAmount = 0.1;
            const string driversName = "name";
            const string driversOccupation = "Accountant";
            const double premium = 500.00;
            var driversDateOfBirth = DateTime.Now;
            var driversClaims = new IClaim[0];
            var jobsWherePremiumIsDecreased = new []{ "Accountant"};
            var driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform = new DriversAndPremiumToDecreasedPremiumBasedOnOccupationTransform(jobsWherePremiumIsDecreased, decreaseAmount);
            var firstDriver = new DriverStub(driversName, driversOccupation, driversDateOfBirth, driversClaims);
            var driverAndPremium = new DriverAndPremium(firstDriver, premium);

            var actual = driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform.Transform(driverAndPremium);

            Assert.AreEqual(expected, actual);
        }
    }
}
