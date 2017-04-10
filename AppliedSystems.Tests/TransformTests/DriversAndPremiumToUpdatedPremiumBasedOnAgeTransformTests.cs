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
    public class DriversAndPremiumToUpdatedPremiumBasedOnAgeTransformTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument()
        {
            var driversAndPremiumToUpdatedPremiumForAdultDrivers = new GenericStubTransform<DriverAndPremium, double>();

            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ObjectCreationAsStatement
            new DriversAndPremiumToUpdatedPremiumBasedOnAgeTransform(null, driversAndPremiumToUpdatedPremiumForAdultDrivers);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument_for_driversAndPremiumToUpdatedPremiumForAdultDrivers()
        {
            var driversAndPremiumToUpdatedPremiumForYoungDrivers = new GenericStubTransform<DriverAndPremium, double>();

            // ReSharper disable once ObjectCreationAsStatement
            // ReSharper disable once AssignNullToNotNullAttribute
            new DriversAndPremiumToUpdatedPremiumBasedOnAgeTransform(driversAndPremiumToUpdatedPremiumForYoungDrivers, null);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_parameter_argument()
        {
            var driversAndPremiumToUpdatedPremiumForAdultDrivers = new GenericStubTransform<DriverAndPremium, double>();
            var driversAndPremiumToUpdatedPremiumForYoungDrivers = new GenericStubTransform<DriverAndPremium, double>();

            var driversAndPremiumToUpdatedPremiumBasedOnAgeTransform = new DriversAndPremiumToUpdatedPremiumBasedOnAgeTransform(driversAndPremiumToUpdatedPremiumForYoungDrivers, driversAndPremiumToUpdatedPremiumForAdultDrivers);
            
            // ReSharper disable once AssignNullToNotNullAttribute
            driversAndPremiumToUpdatedPremiumBasedOnAgeTransform.Transform(null);
        }

        [TestMethod]
        public void Should_return_result_of_premium_after_adult_calculation()
        {
            const string name = "name";
            const string occupation = "occupation";
            const double expected = 660.00;
            const double premium = 500.00;
            var claims = new IClaim[0];
            var dateOfBirth = DateTime.Now;
            var driversAndPremiumToUpdatedPremiumForAdultDrivers = new GenericStubTransform<DriverAndPremium, double>(expected);
            var driversAndPremiumToUpdatedPremiumForYoungDrivers = new GenericStubTransform<DriverAndPremium, double>(expected);
            var driversAndPremiumToUpdatedPremiumBasedOnAgeTransform = new DriversAndPremiumToUpdatedPremiumBasedOnAgeTransform(driversAndPremiumToUpdatedPremiumForYoungDrivers, driversAndPremiumToUpdatedPremiumForAdultDrivers);
            var driver = new DriverStub(name, occupation, dateOfBirth, claims);

            var actual = driversAndPremiumToUpdatedPremiumBasedOnAgeTransform.Transform(new DriverAndPremium(driver, premium));

            Assert.AreEqual(expected, actual);
        }
    }
}
