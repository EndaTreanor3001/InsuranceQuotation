using System;
using AppliedSystems.Dtos.Stubs;
using AppliedSystems.Interfaces;
using AppliedSystems.Transforms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppliedSystems.Tests.TransformTests
{
    [TestClass]
    public class RejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransformTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_argument()
        {
            var rejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform =  new RejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform();

            // ReSharper disable once AssignNullToNotNullAttribute
            rejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform.Transform(null);
        }

        [TestMethod]
        public void Should_value_return_true_for_success_when_there_are_no_claims()
        {
            var rejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform =  new RejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform();

            var actual = rejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform.Transform(new IDriver[0]).Success;

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Should_value_return_true_for_success_when_no_driver_has_more_than_two_claims()
        {
            const string name = "997CA129-4C94-42E0-A1A8-CBBE7428BDD1";
            const string occupation = "91F6F29D-6D52-459F-9B03-FCA10749E12D";
            const string secondDriversName = "E99BD141-22A2-45E2-885C-380234066C25";
            var now = DateTime.Now;
            var firstClaim = new ClaimStub(now);
            var secondClaim = new ClaimStub(now);
            var rejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform =  new RejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform();
            var firstDriver = new DriverStub(name, occupation, now, new []{ firstClaim, secondClaim  });
            var secondDriver = new DriverStub(secondDriversName, occupation, now, new []{ firstClaim, secondClaim  });
            var drivers = new []{ firstDriver, secondDriver };
            var actual = rejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform.Transform(drivers).Success;

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Should_value_return_false_for_success_when_second_driver_has_more_than_two_claims()
        {
            const string name = "997CA129-4C94-42E0-A1A8-CBBE7428BDD1";
            const string occupation = "91F6F29D-6D52-459F-9B03-FCA10749E12D";
            const string secondDriversName = "E99BD141-22A2-45E2-885C-380234066C25";
            var now = DateTime.Now;
            var firstClaim = new ClaimStub(now);
            var secondClaim = new ClaimStub(now);
            var thirdClaim = new ClaimStub(now);
            var rejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform =  new RejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform();
            var firstDriver = new DriverStub(name, occupation, now, new []{ firstClaim, secondClaim  });
            var secondDriver = new DriverStub(secondDriversName, occupation, now, new []{ firstClaim, secondClaim, thirdClaim });
            var drivers = new []{ firstDriver, secondDriver };
            var actual = rejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform.Transform(drivers).Success;

            Assert.IsFalse(actual);
        }
    }
}
