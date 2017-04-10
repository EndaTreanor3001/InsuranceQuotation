using System;
using System.Linq;
using AppliedSystems.Dtos;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Transforms
{
    public class PolicyToResultTransform : ITransform<IPolicy, string>
    {
        private readonly double _startingPremium;
        private readonly ITransform<IPolicy, RejectionMessageAndSuccess> _policyToRejectionMessageAndSuccess;
        private readonly ITransform<DriverAndPremium, double> _driversAndPremiumToUpdatedPremiumBasedOnOccupationTransform;
        private readonly ITransform<DriverAndPremium, double> _driversAndPremiumToUpdatedPremiumBasedOnAgeTransform;
        private readonly ITransform<DriverAndPremium, double> _driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform;

        public PolicyToResultTransform(double startingPremium, 
                                       [NotNull] ITransform<IPolicy, RejectionMessageAndSuccess> policyToRejectionMessageAndSuccess, 
                                       [NotNull] ITransform<DriverAndPremium, double> driversAndPremiumToUpdatedPremiumBasedOnOccupationTransform,
                                       [NotNull] ITransform<DriverAndPremium, double> driversAndPremiumToUpdatedPremiumBasedOnAgeTransform,
                                       [NotNull] ITransform<DriverAndPremium, double> driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform )
        {
            if (policyToRejectionMessageAndSuccess == null) throw new ArgumentNullException(nameof(policyToRejectionMessageAndSuccess));
            if (driversAndPremiumToUpdatedPremiumBasedOnOccupationTransform == null) throw new ArgumentNullException(nameof(driversAndPremiumToUpdatedPremiumBasedOnOccupationTransform));
            if (driversAndPremiumToUpdatedPremiumBasedOnAgeTransform == null) throw new ArgumentNullException(nameof(driversAndPremiumToUpdatedPremiumBasedOnAgeTransform));
            if (driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform == null) throw new ArgumentNullException(nameof(driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform));

            _startingPremium = startingPremium;
            _policyToRejectionMessageAndSuccess = policyToRejectionMessageAndSuccess;
            _driversAndPremiumToUpdatedPremiumBasedOnOccupationTransform = driversAndPremiumToUpdatedPremiumBasedOnOccupationTransform;
            _driversAndPremiumToUpdatedPremiumBasedOnAgeTransform = driversAndPremiumToUpdatedPremiumBasedOnAgeTransform;
            _driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform = driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform;
        }

        public string Transform(IPolicy policy)
        {
            if (policy == null) throw new ArgumentNullException(nameof(policy));

            var policyDrivers = policy.Drivers.ToArray();

            var rejectionMessageAndSuccess = _policyToRejectionMessageAndSuccess.Transform(policy);

            if (!rejectionMessageAndSuccess.Success) return rejectionMessageAndSuccess.RejectionMessage;

            var premium = 0.00;

            foreach (var policyDriver in policyDrivers)
            {
                var premiumUpdatedBasedOnOccupation = _driversAndPremiumToUpdatedPremiumBasedOnOccupationTransform.Transform(new DriverAndPremium(policyDriver, _startingPremium));

                var premiumUpdatedBasedOnAge = _driversAndPremiumToUpdatedPremiumBasedOnAgeTransform.Transform(new DriverAndPremium(policyDriver, premiumUpdatedBasedOnOccupation));

                var premiumBasedOnDriverClaims = _driversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform.Transform(new DriverAndPremium(policyDriver, premiumUpdatedBasedOnAge));

                premium += premiumBasedOnDriverClaims;
            }
            return $"Premium quotation is {premium}";
        }
    }
}
