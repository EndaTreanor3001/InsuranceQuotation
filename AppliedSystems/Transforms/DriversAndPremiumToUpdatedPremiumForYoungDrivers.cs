using System;
using AppliedSystems.Dtos;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Transforms
{
    public class DriversAndPremiumToUpdatedPremiumForYoungDrivers : ITransform<DriverAndPremium, double>
    {
        private readonly int _startingYoungAge;
        private readonly int _endingYoungAge;
        private readonly IProvide<DateTime> _todayProvider;
        private readonly double _increasePremiumValue;

        public DriversAndPremiumToUpdatedPremiumForYoungDrivers(int startingYoungAge, int endingYoungAge, [NotNull] IProvide<DateTime> todayProvider, double increasePremiumValue)
        {
            if (todayProvider == null) throw new ArgumentNullException(nameof(todayProvider));

            _startingYoungAge = startingYoungAge;
            _endingYoungAge = endingYoungAge;
            _todayProvider = todayProvider;
            _increasePremiumValue = increasePremiumValue;
        }

        public double Transform([NotNull] DriverAndPremium driverAndPremium)
        {
            if (driverAndPremium == null) throw new ArgumentNullException(nameof(driverAndPremium));

            var driver = driverAndPremium.Driver;
            var premium = driverAndPremium.Premium;

            var today = _todayProvider.Get();
            if (driver.DateOfBirth < today.AddYears(-_startingYoungAge) && driver.DateOfBirth > today.AddYears(-_endingYoungAge))
            {
                premium += premium * _increasePremiumValue;
            }

            return premium;
        }
    }
}
