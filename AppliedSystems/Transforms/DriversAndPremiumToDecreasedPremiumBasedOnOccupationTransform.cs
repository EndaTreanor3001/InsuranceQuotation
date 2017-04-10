using System;
using System.Collections.Generic;
using System.Linq;
using AppliedSystems.Dtos;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Transforms
{
    public class DriversAndPremiumToDecreasedPremiumBasedOnOccupationTransform : ITransform<DriverAndPremium, double>
    {
        private readonly IEnumerable<string> _jobsWherePremiumIsDecreased;
        private readonly double _decreaseAmount;

        public DriversAndPremiumToDecreasedPremiumBasedOnOccupationTransform([NotNull] IEnumerable<string> jobsWherePremiumIsDecreased, double decreaseAmount)
        {
            if (jobsWherePremiumIsDecreased == null) throw new ArgumentNullException(nameof(jobsWherePremiumIsDecreased));

            _jobsWherePremiumIsDecreased = jobsWherePremiumIsDecreased;
            _decreaseAmount = decreaseAmount;
        }

        public double Transform([NotNull] DriverAndPremium driverAndPremium)
        {
            if (driverAndPremium == null) throw new ArgumentNullException(nameof(driverAndPremium));
            var driver = driverAndPremium.Driver;
            var premium = driverAndPremium.Premium;

            if (_jobsWherePremiumIsDecreased.Any(o => o == driver.Occupation)) premium -= premium * _decreaseAmount;          

            return premium;
        }
    }
}