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
    public class DriversAndPremiumToUpdatedPremiumBasedOnOccupationTransformTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument_for_driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform()
        {
            var driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform = new GenericStubTransform<DriverAndPremium, double>();
            // ReSharper disable once ObjectCreationAsStatement
            // ReSharper disable once AssignNullToNotNullAttribute
            new DriversAndPremiumToUpdatedPremiumBasedOnOccupationTransform(null, driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument_for_driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform()
        {
            var driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform = new GenericStubTransform<DriverAndPremium, double>();
            // ReSharper disable once ObjectCreationAsStatement
            // ReSharper disable once AssignNullToNotNullAttribute
            new DriversAndPremiumToUpdatedPremiumBasedOnOccupationTransform(driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform, null);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_argument()
        {
            var driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform = new GenericStubTransform<DriverAndPremium, double>();
            var driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform = new GenericStubTransform<DriverAndPremium, double>();

            var driversAndPremiumToUpdatedPremiumBasedOnOccupationTransform = new DriversAndPremiumToUpdatedPremiumBasedOnOccupationTransform(driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform, driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform);

            driversAndPremiumToUpdatedPremiumBasedOnOccupationTransform.Transform(null);
        }

        [TestMethod]
        public void Should_return_the_result_of_driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform()
        {
            const double premium = 500.00;
            const string driverName = "name";
            const string driverOccupation = "occupation";
            var driversDateOfBirth = DateTime.Now;
            var claims = new IClaim[0];
            var drivers = new DriverStub(driverName, driverOccupation, driversDateOfBirth, claims);
            const double expected = 660.00;
            var driversAndPremium = new DriverAndPremium(drivers, premium );
            var driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform = new GenericStubTransform<DriverAndPremium, double>(premium);
            var driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform = new GenericStubTransform<DriverAndPremium, double>(expected);
            var driversAndPremiumToUpdatedPremiumBasedOnOccupationTransform = new DriversAndPremiumToUpdatedPremiumBasedOnOccupationTransform(driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform, driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform);

            var actual = driversAndPremiumToUpdatedPremiumBasedOnOccupationTransform.Transform(driversAndPremium);

            Assert.AreEqual(expected, actual);
        }
    }
}
