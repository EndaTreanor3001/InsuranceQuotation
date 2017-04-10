using System;
using AppliedSystems.Dtos;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Transforms
{
    public class RejectionMessageAndSuccessBasedOnPolicyStartDateTransform : ITransform<DateTime, RejectionMessageAndSuccess>
    {
        private readonly IProvide<DateTime> _todayProvider;

        public RejectionMessageAndSuccessBasedOnPolicyStartDateTransform([NotNull] IProvide<DateTime> todayProvider)
        {
            if (todayProvider == null) throw new ArgumentNullException(nameof(todayProvider));

            _todayProvider = todayProvider;
        }

        public RejectionMessageAndSuccess Transform(DateTime policyStartDate)
        {
            var today = _todayProvider.Get();

            if(today > policyStartDate) return new RejectionMessageAndSuccess("Start Date of Policy", false);

            return new RejectionMessageAndSuccess(string.Empty, true);
        }
    }
}