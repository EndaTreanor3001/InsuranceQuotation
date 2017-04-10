using System;
using AppliedSystems.GenericStubs;
using AppliedSystems.Transforms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppliedSystems.Tests.TransformTests
{
    [TestClass]
    public class RejectionMessageAndSuccessBasedOnPolicyStartDateTransformTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_fail_on_null_constructor_argument()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ObjectCreationAsStatement
            new RejectionMessageAndSuccessBasedOnPolicyStartDateTransform(null);
        }

        [TestMethod]
        public void Should_return_false_for_success_when_today_is_greater_than_policy_start_date()
        {
            var today = DateTime.Now;
            var nowTimeProvider = new GenericStubProvider<DateTime>(today);
            var rejectionMessageAndSuccessBasedOnPolicyStartDateTransform = new RejectionMessageAndSuccessBasedOnPolicyStartDateTransform(nowTimeProvider);

            var actual = rejectionMessageAndSuccessBasedOnPolicyStartDateTransform.Transform(DateTime.MinValue).Success;

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Should_return_true_for_success_when_today_is_equal_to_policy_start_date()
        {
            var today = DateTime.Today;
            var nowTimeProvider = new GenericStubProvider<DateTime>(today);
            var rejectionMessageAndSuccessBasedOnPolicyStartDateTransform = new RejectionMessageAndSuccessBasedOnPolicyStartDateTransform(nowTimeProvider);

            var actual = rejectionMessageAndSuccessBasedOnPolicyStartDateTransform.Transform(today).Success;

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Should_return_true_for_success_when_today_is_before_than_policy_start_date()
        {
            var today = DateTime.Today;
            var nowTimeProvider = new GenericStubProvider<DateTime>(today);
            var rejectionMessageAndSuccessBasedOnPolicyStartDateTransform = new RejectionMessageAndSuccessBasedOnPolicyStartDateTransform(nowTimeProvider);

            var actual = rejectionMessageAndSuccessBasedOnPolicyStartDateTransform.Transform(DateTime.MaxValue).Success;

            Assert.IsTrue(actual);
        }
    }
}
