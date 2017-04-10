using System;
using System.Collections.Generic;
using AppliedSystems.Dtos;
using AppliedSystems.Dtos.Stubs;
using AppliedSystems.GenericStubs;
using AppliedSystems.Interfaces;
using AppliedSystems.Transforms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppliedSystems.Tests.TransformTests
{
    [TestClass]
    public class PolicyToRejectionMessageAndSuccessTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument()
        {
            var rejectionMessageAndSuccessBasedOnPolicyStartDateTransform = new GenericStubTransform<DateTime, RejectionMessageAndSuccess>();
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ObjectCreationAsStatement
            new PolicyToRejectionMessageAndSuccess(rejectionMessageAndSuccessBasedOnPolicyStartDateTransform, null );
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument_for_rejectionMessageAndSuccessBasedOnPolicyStartDateTransform()
        {
            var firstTransform = new GenericStubTransform<IEnumerable<IDriver>, RejectionMessageAndSuccess>();
            var driverBasedRejectionMessagesAndSuccessTransforms = new []{ firstTransform };
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ObjectCreationAsStatement
            new PolicyToRejectionMessageAndSuccess(null, driverBasedRejectionMessagesAndSuccessTransforms );
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_argument()
        {
            var rejectionMessageAndSuccessBasedOnPolicyStartDateTransform = new GenericStubTransform<DateTime, RejectionMessageAndSuccess>();
            var firstTransform = new GenericStubTransform<IEnumerable<IDriver>, RejectionMessageAndSuccess>();
            var driverBasedRejectionMessagesAndSuccessTransforms = new []{ firstTransform };

            var policyToRejectionMessageAndSuccess = new PolicyToRejectionMessageAndSuccess(rejectionMessageAndSuccessBasedOnPolicyStartDateTransform, driverBasedRejectionMessagesAndSuccessTransforms );

            // ReSharper disable once AssignNullToNotNullAttribute
            policyToRejectionMessageAndSuccess.Transform(null);
        }

        [TestMethod]
        public void Should_return_rejection_based_on_policy_date_when_that_rejection_message_and_success_is_false()
        {
            var expected = new RejectionMessageAndSuccess("Start Date of Policy", false);
            var rejectionMessageAndSuccessBasedOnPolicyStartDateTransform = new GenericStubTransform<DateTime, RejectionMessageAndSuccess>(expected);
            var firstTransform = new GenericStubTransform<IEnumerable<IDriver>, RejectionMessageAndSuccess>();
            var driverBasedRejectionMessagesAndSuccessTransforms = new []{ firstTransform };
            var policyToRejectionMessageAndSuccess = new PolicyToRejectionMessageAndSuccess(rejectionMessageAndSuccessBasedOnPolicyStartDateTransform, driverBasedRejectionMessagesAndSuccessTransforms );
            var now = DateTime.Now;
            var claims = new []{ new ClaimStub(now) };
            var firstDriver = new DriverStub("name", "occupation", now, claims);
            var drivers = new [] { firstDriver };
            var policy = new PolicyStub(now, drivers);

            var actual = policyToRejectionMessageAndSuccess.Transform(policy);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_return_rejection_based_a_driver_when_that_rejection_message_and_success_is_false()
        {
            var expected = new RejectionMessageAndSuccess("58AAD435-3CD3-43E9-95EB-1A4B5361DE9E", false);
            var rejectionMessageAndSuccess = new RejectionMessageAndSuccess("passed message", true);
            var rejectionMessageAndSuccessBasedOnPolicyStartDateTransform = new GenericStubTransform<DateTime, RejectionMessageAndSuccess>(new RejectionMessageAndSuccess("Start Date of Policy", true));
            var firstTransform = new GenericStubTransform<IEnumerable<IDriver>, RejectionMessageAndSuccess>(rejectionMessageAndSuccess);
            var secondTransform = new GenericStubTransform<IEnumerable<IDriver>, RejectionMessageAndSuccess>(expected);
            var driverBasedRejectionMessagesAndSuccessTransforms = new []{ firstTransform, secondTransform };
            var policyToRejectionMessageAndSuccess = new PolicyToRejectionMessageAndSuccess(rejectionMessageAndSuccessBasedOnPolicyStartDateTransform, driverBasedRejectionMessagesAndSuccessTransforms );
            var now = DateTime.Now;
            var claims = new []{ new ClaimStub(now) };
            var firstDriver = new DriverStub("name", "occupation", now, claims);
            var drivers = new [] { firstDriver };
            var policy = new PolicyStub(now, drivers);

            var actual = policyToRejectionMessageAndSuccess.Transform(policy);

            Assert.AreEqual(expected, actual);
        }
    }
}
