using System;
using System.Linq;
using AppliedSystems.Dtos;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Transforms
{
    public class DriversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform : ITransform<DriverAndPremium, double>
    {
        private readonly double _smallerPercentIncrease;
        private readonly double _largePercentageIncrease;
        private readonly int _smallerTimeSpanInYears;
        private readonly int _largerTimeSpanInYears;
        private readonly IProvide<DateTime> _todayProvider;

        public DriversAndPremiumToUpdatedPremiumBasedOnDriverClaimsTransform(double smallerPercentIncrease, 
                                                                             double largePercentageIncrease, 
                                                                             int smallerTimeSpanInYears, 
                                                                             int largerTimeSpanInYears,
                                                                             [NotNull] IProvide<DateTime> todayProvider )
        {
            if (todayProvider == null) throw new ArgumentNullException(nameof(todayProvider));

            _smallerPercentIncrease = smallerPercentIncrease;
            _largePercentageIncrease = largePercentageIncrease;
            _smallerTimeSpanInYears = smallerTimeSpanInYears;
            _largerTimeSpanInYears = largerTimeSpanInYears;
            _todayProvider = todayProvider;
        }

        public double Transform([NotNull] DriverAndPremium driverAndPremium)
        {
            if (driverAndPremium == null) throw new ArgumentNullException(nameof(driverAndPremium));

            var driver = driverAndPremium.Driver;
            var premium = driverAndPremium.Premium;
            var today = _todayProvider.Get();

            var claimsWithinSmallerTimeFrame = driver.Claims.Where(a => a.DateOfClaim > today.AddYears(-_smallerTimeSpanInYears));
            var claimsWithinLargerTimeFrame = driver.Claims.Where(a => a.DateOfClaim < today.AddYears(-_smallerTimeSpanInYears) && a.DateOfClaim > today.AddYears(-_largerTimeSpanInYears));

            premium = claimsWithinSmallerTimeFrame.Aggregate(premium, (o, a) => o + o * _largePercentageIncrease);

            return claimsWithinLargerTimeFrame.Aggregate(premium, (o, a) => o + o * _smallerPercentIncrease);
        }
    }
}
