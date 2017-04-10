using System;
using System.Collections.Generic;
using System.Linq;
using AppliedSystems.Dtos;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Transforms
{
    public class DriversAndPremiumToIncreasedPremiumBasedOnOccupationTransform : ITransform<DriverAndPremium, double>
    {
        private readonly IEnumerable<string> _jobsWherePremiumIsIncreased;
        private readonly double _increaseAmount;

        public DriversAndPremiumToIncreasedPremiumBasedOnOccupationTransform([NotNull] IEnumerable<string> jobsWherePremiumIsIncreased, double increaseAmount)
        {
            if (jobsWherePremiumIsIncreased == null) throw new ArgumentNullException(nameof(jobsWherePremiumIsIncreased));

            _jobsWherePremiumIsIncreased = jobsWherePremiumIsIncreased;
            _increaseAmount = increaseAmount;
        }

        public double Transform([NotNull] DriverAndPremium driverAndPremium)
        {
            if (driverAndPremium == null) throw new ArgumentNullException(nameof(driverAndPremium));

            var driver = driverAndPremium.Driver;
            var premium = driverAndPremium.Premium;

            if (_jobsWherePremiumIsIncreased.Any(o => o == driver.Occupation)) premium += premium * _increaseAmount;

            return premium;
        }
    }
}