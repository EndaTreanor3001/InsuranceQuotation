using System;
using AppliedSystems.Dtos.Stubs;
using AppliedSystems.GenericStubs;
using AppliedSystems.Interfaces;
using AppliedSystems.Transforms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppliedSystems.Tests.TransformTests
{
    [TestClass]
    public class RejectionMessageAndSuccessBasedOnOldestDriverTransformTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument()
        {    
            // ReSharper disable once ObjectCreationAsStatement
            // ReSharper disable once AssignNullToNotNullAttribute
            new RejectionMessageAndSuccessBasedOnOldestDriverTransform(75, null);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_parameter_argument()
        {
            var nowTimeProvider = new GenericStubProvider<DateTime>();
            var rejectionMessageAndSuccessBasedOnOldestDriverTransform = new RejectionMessageAndSuccessBasedOnOldestDriverTransform(75, nowTimeProvider);

            // ReSharper disable once AssignNullToNotNullAttribute
            rejectionMessageAndSuccessBasedOnOldestDriverTransform.Transform(null);
        }

        [TestMethod]
        public void Should_return_true_for_success_when_there_are_no_drivers()
        {
            var nowTimeProvider = new GenericStubProvider<DateTime>();
            var rejectionMessageAndSuccessBasedOnOldestDriverTransform = new RejectionMessageAndSuccessBasedOnOldestDriverTransform(75,nowTimeProvider);
            var drivers = new IDriver[0];
            var actual = rejectionMessageAndSuccessBasedOnOldestDriverTransform.Transform(drivers).Success;

            Assert.IsTrue(actual);

        }

        [TestMethod]
        public void Should_return_false_for_success_when_a_driver_is_older_than_75()
        {
            var now = DateTime.Now;
            var nowTimeProvider = new GenericStubProvider<DateTime>(now);
            const string drivername = "driverName";
            const string occupation = "occupation";
            var dateOfBirth70 = DateTime.Now.AddYears(-70);
            var driverOneClaim = new ClaimStub(DateTime.Now);
            var firstDriver = new DriverStub(drivername, occupation, dateOfBirth70, new []{ driverOneClaim } );
            var dateOfBirth76 = DateTime.Now.AddYears(-76);
            var driver2Claim = new ClaimStub(DateTime.Now);
            var secondDriver = new DriverStub(drivername, occupation, dateOfBirth76, new []{ driver2Claim } );
            var rejectionMessageAndSuccessBasedOnOldestDriverTransform = new RejectionMessageAndSuccessBasedOnOldestDriverTransform(75, nowTimeProvider);

            var drivers = new []{firstDriver, secondDriver};
            var actual = rejectionMessageAndSuccessBasedOnOldestDriverTransform.Transform(drivers).Success;

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Should_return_true_for_success_when_no_driver_is_older_than_75()
        {
            var now = DateTime.Now;
            var nowTimeProvider = new GenericStubProvider<DateTime>(now);
            const string drivername = "driverName";
            const string occupation = "occupation";
            var dateOfBirth70 = DateTime.Now.AddYears(-70);
            var driverOneClaim = new ClaimStub(DateTime.Now);
            var firstDriver = new DriverStub(drivername, occupation, dateOfBirth70, new []{ driverOneClaim } );
            var dateOfBirth74 = DateTime.Now.AddYears(-74);
            var driver2Claim = new ClaimStub(DateTime.Now);
            var secondDriver = new DriverStub(drivername, occupation, dateOfBirth74, new []{ driver2Claim } );
            var rejectionMessageAndSuccessBasedOnOldestDriverTransform = new RejectionMessageAndSuccessBasedOnOldestDriverTransform(75, nowTimeProvider);

            var drivers = new []{firstDriver, secondDriver};
            var actual = rejectionMessageAndSuccessBasedOnOldestDriverTransform.Transform(drivers).Success;

            Assert.IsTrue(actual);
        }
    }
}
