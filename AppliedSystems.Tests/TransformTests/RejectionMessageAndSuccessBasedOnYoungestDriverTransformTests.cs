using System;
using AppliedSystems.Dtos.Stubs;
using AppliedSystems.GenericStubs;
using AppliedSystems.Transforms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppliedSystems.Tests.TransformTests
{
    [TestClass]
    public class RejectionMessageAndSuccessBasedOnYoungestDriverTransformTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ObjectCreationAsStatement
            new RejectionMessageAndSuccessBasedOnYoungestDriverTransform(21, null);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_parameter_argument()
        {
            var todayProvider = new GenericStubProvider<DateTime>();
            var rejectionMessageAndSuccessBasedOnYoungestDriverTransform = new RejectionMessageAndSuccessBasedOnYoungestDriverTransform(21, todayProvider);

            // ReSharper disable once AssignNullToNotNullAttribute
            rejectionMessageAndSuccessBasedOnYoungestDriverTransform.Transform(null);
        }

        [TestMethod]
        public void Should_return_true_for_success_when_youngestDriver_is_older_than_21()
        {
            const string name = "name";
            const string occupation = "occupation";
            var today = DateTime.Today;
            var todayProvider = new GenericStubProvider<DateTime>(today);
            var dateOfBirth = DateTime.Now.AddYears(-22);
            var rejectionMessageAndSuccessBasedOnYoungestDriverTransform = new RejectionMessageAndSuccessBasedOnYoungestDriverTransform(21, todayProvider);
            var firstDriversFirstClaim = new ClaimStub(DateTime.Now);
            var firstDriversClaims = new [] { firstDriversFirstClaim };
            var secondDriversFirstClaim = new ClaimStub(DateTime.Now);
            var secondDriversClaims = new [] { secondDriversFirstClaim };
            var firstDriver = new DriverStub(name, occupation, dateOfBirth, firstDriversClaims );
            var secondDriver = new DriverStub(name, occupation, dateOfBirth, secondDriversClaims );
            var drivers = new []{ firstDriver, secondDriver };

            var actual = rejectionMessageAndSuccessBasedOnYoungestDriverTransform.Transform(drivers).Success;

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Should_return_true_for_success_when_youngestDriver_is_than_21()
        {
            const string name = "name";
            const string occupation = "occupation";
            var today = DateTime.Today;
            var todayProvider = new GenericStubProvider<DateTime>(today);
            var dateOfBirth = today.AddYears(-21);
            var rejectionMessageAndSuccessBasedOnYoungestDriverTransform = new RejectionMessageAndSuccessBasedOnYoungestDriverTransform(21, todayProvider);
            var firstDriversFirstClaim = new ClaimStub(DateTime.Now);
            var firstDriversClaims = new [] { firstDriversFirstClaim };
            var secondDriversFirstClaim = new ClaimStub(DateTime.Now);
            var secondDriversClaims = new [] { secondDriversFirstClaim };
            var firstDriver = new DriverStub(name, occupation, dateOfBirth, firstDriversClaims );
            var secondDriver = new DriverStub(name, occupation, dateOfBirth, secondDriversClaims );
            var drivers = new []{ firstDriver, secondDriver };

            var actual = rejectionMessageAndSuccessBasedOnYoungestDriverTransform.Transform(drivers).Success;

            Assert.IsTrue(actual);
        }
    }
}
