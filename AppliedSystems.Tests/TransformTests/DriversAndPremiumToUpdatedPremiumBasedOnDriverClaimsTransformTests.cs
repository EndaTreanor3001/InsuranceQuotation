using System;
using AppliedSystems.Dtos;
using AppliedSystems.Dtos.Stubs;
using AppliedSystems.GenericStubs;
using AppliedSystems.Transforms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppliedSystems.Tests.TransformTests
{
    [TestClass]
    public class DriversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransformTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ObjectCreationAsStatement
            new DriversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform(0.1, 0.2, 1, 5, null);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_argument()
        {
            var todayProvider = new GenericStubProvider<DateTime>();
            const double smallerPercentIncrease = 0.1;
            const double largePercentageIncrease = 0.2;
            const int smallerTimeSpanInYears = 1;
            const int largerTimeSpanInYears = 5;
            var driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform = new DriversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform(smallerPercentIncrease, largePercentageIncrease, smallerTimeSpanInYears, largerTimeSpanInYears, todayProvider);

            // ReSharper disable once AssignNullToNotNullAttribute
            driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform.Transform(null);
        }

        [TestMethod]
        public void Should_increase_premium_by_larger_percentage_when_a_claim_has_been_made_within_one_year()
        {
            const double expected = 600.00;
            const string name = "name";
            const string occupation = "occupation";
            const double premium = 500.00;
            var dateOfBirth = DateTime.Now;
            var dateOfClaim = DateTime.Today;
            var todayProvider = new GenericStubProvider<DateTime>(DateTime.Today);
            const double smallerPercentIncrease = 0.1;
            const double largePercentageIncrease = 0.2;
            const int smallerTimeSpanInYears = 1;
            const int largerTimeSpanInYears = 5;
            var driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform = new DriversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform(smallerPercentIncrease, largePercentageIncrease, smallerTimeSpanInYears, largerTimeSpanInYears, todayProvider);
            var claim = new ClaimStub(dateOfClaim);
            var driver = new DriverStub(name, occupation, dateOfBirth, new [] { claim });
            var driverAndPremium = new DriverAndPremium(driver, premium);

            var actual =  driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform.Transform(driverAndPremium);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_increase_premium_by_smaller_percentage_when_a_claim_has_been_made_more_than_one_year_ago()
        {
            const double expected = 550.00;
            const string name = "name";
            const string occupation = "occupation";
            const double premium = 500.00;
            var dateOfBirth = DateTime.Now;
            var dateOfClaim = DateTime.Today.AddYears(-2);
            var todayProvider = new GenericStubProvider<DateTime>(DateTime.Today);
            const double smallerPercentIncrease = 0.1;
            const double largePercentageIncrease = 0.2;
            const int smallerTimeSpanInYears = 1;
            const int largerTimeSpanInYears = 5;
            var driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform = new DriversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform(smallerPercentIncrease, largePercentageIncrease, smallerTimeSpanInYears, largerTimeSpanInYears, todayProvider);
            var claim = new ClaimStub(dateOfClaim);
            var driver = new DriverStub(name, occupation, dateOfBirth, new [] { claim });
            var driversAndPremium = new DriverAndPremium(driver, premium);

            var actual =  driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform.Transform(driversAndPremium);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_increase_premium_by_smaller_percentage_twice_when_both_claims_have_been_made_more_than_one_year_ago()
        {
            const double expected = 605.00;
            const string name = "name";
            const string occupation = "occupation";
            const double premium = 500.00;
            var dateOfBirth = DateTime.Now;
            var dateOfClaim = DateTime.Today.AddYears(-2);
            var todayProvider = new GenericStubProvider<DateTime>(DateTime.Today);
            const double smallerPercentIncrease = 0.1;
            const double largePercentageIncrease = 0.2;
            const int smallerTimeSpanInYears = 1;
            const int largerTimeSpanInYears = 5;
            var driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform = new DriversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform(smallerPercentIncrease, largePercentageIncrease, smallerTimeSpanInYears, largerTimeSpanInYears, todayProvider);
            var claim = new ClaimStub(dateOfClaim);
            var secondClaim = new ClaimStub(dateOfClaim);
            var driver = new DriverStub(name, occupation, dateOfBirth, new [] { claim, secondClaim });
            var driversAndPremium = new DriverAndPremium(driver, premium);

            var actual =  driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform.Transform(driversAndPremium);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_increase_premium_by_smaller_percentage_twice_when_both_claims_have_been_made_within_one_year_ago()
        {
            const double expected = 720.00;
            const string name = "name";
            const string occupation = "occupation";
            const double premium = 500.00;
            var dateOfBirth = DateTime.Now;
            var dateOfClaim = DateTime.Today;
            var todayProvider = new GenericStubProvider<DateTime>(DateTime.Today);
            const double smallerPercentIncrease = 0.1;
            const double largePercentageIncrease = 0.2;
            const int smallerTimeSpanInYears = 1;
            const int largerTimeSpanInYears = 5;
            var driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform = new DriversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform(smallerPercentIncrease, largePercentageIncrease, smallerTimeSpanInYears, largerTimeSpanInYears, todayProvider);
            var claim = new ClaimStub(dateOfClaim);
            var secondClaim = new ClaimStub(dateOfClaim);
            var driver = new DriverStub(name, occupation, dateOfBirth, new [] { claim, secondClaim });
            var driversAndPremium = new DriverAndPremium(driver, premium);

            var actual =  driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform.Transform(driversAndPremium);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Should_increase_the_premium_by_both_percentage_amounts_when_a_claim_has_been_made_within_a_year_and_the_another_was_made_more_than_one_year_ago()
        {
            const double expected = 660.00;
            const string name = "name";
            const string occupation = "occupation";
            const double premium = 500.00;
            var dateOfBirth = DateTime.Now;
            var dateOfFirstClaim = DateTime.Today;
            var dateOfSecondClaim = DateTime.Today.AddMonths(-18);
            var todayProvider = new GenericStubProvider<DateTime>(DateTime.Today);
            const double smallerPercentIncrease = 0.1;
            const double largePercentageIncrease = 0.2;
            const int smallerTimeSpanInYears = 1;
            const int largerTimeSpanInYears = 5;
            var driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform = new DriversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform(smallerPercentIncrease, largePercentageIncrease, smallerTimeSpanInYears, largerTimeSpanInYears, todayProvider);
            var claim = new ClaimStub(dateOfFirstClaim);
            var secondClaim = new ClaimStub(dateOfSecondClaim);
            var driver = new DriverStub(name, occupation, dateOfBirth, new [] { claim, secondClaim });
            var driversAndPremium = new DriverAndPremium(driver, premium);

            var actual =  driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform.Transform(driversAndPremium);

            Assert.AreEqual(expected, actual);
        }
    }
}
