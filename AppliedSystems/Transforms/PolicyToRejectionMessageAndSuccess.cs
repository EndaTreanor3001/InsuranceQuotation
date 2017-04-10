using System;
using System.Collections.Generic;
using System.Linq;
using AppliedSystems.Dtos;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Transforms
{
    public class PolicyToRejectionMessageAndSuccess : ITransform<IPolicy, RejectionMessageAndSuccess>
    {
        private readonly ITransform<DateTime, RejectionMessageAndSuccess> _rejectionMessageAndSuccessBasedOnPolicyStartDateTransform;
        private readonly IEnumerable<ITransform<IEnumerable<IDriver>, RejectionMessageAndSuccess>> _driverBasedRejectionMessagesAndSuccessTransforms;

        public PolicyToRejectionMessageAndSuccess([NotNull] ITransform<DateTime, RejectionMessageAndSuccess> rejectionMessageAndSuccessBasedOnPolicyStartDateTransform,
                                                  [NotNull] IEnumerable<ITransform<IEnumerable<IDriver>, RejectionMessageAndSuccess>> driverBasedRejectionMessagesAndSuccessTransforms)
        {
            if (rejectionMessageAndSuccessBasedOnPolicyStartDateTransform == null) throw new ArgumentNullException(nameof(rejectionMessageAndSuccessBasedOnPolicyStartDateTransform));
            if (driverBasedRejectionMessagesAndSuccessTransforms == null) throw new ArgumentNullException(nameof(driverBasedRejectionMessagesAndSuccessTransforms));

            _rejectionMessageAndSuccessBasedOnPolicyStartDateTransform = rejectionMessageAndSuccessBasedOnPolicyStartDateTransform;
            _driverBasedRejectionMessagesAndSuccessTransforms = driverBasedRejectionMessagesAndSuccessTransforms;
        }

        public RejectionMessageAndSuccess Transform([NotNull] IPolicy policy)
        {
            if (policy == null) throw new ArgumentNullException(nameof(policy));

            var policyDrivers = policy.Drivers.ToArray();
            var policyStartDate = policy.StartDate;

            var rejectionMessageAndSuccessBasedOnPolicyStartDate = _rejectionMessageAndSuccessBasedOnPolicyStartDateTransform.Transform(policyStartDate);
            if(!rejectionMessageAndSuccessBasedOnPolicyStartDate.Success) return rejectionMessageAndSuccessBasedOnPolicyStartDate;

            foreach (var driverBasedRejectionMessagesAndSuccessTransform in _driverBasedRejectionMessagesAndSuccessTransforms)
            {
                var rejectionMessageAndSuccess = driverBasedRejectionMessagesAndSuccessTransform.Transform(policyDrivers);
                if(!rejectionMessageAndSuccess.Success) return rejectionMessageAndSuccess;
            }

            return new RejectionMessageAndSuccess(string.Empty, true);
        }
    }
}
