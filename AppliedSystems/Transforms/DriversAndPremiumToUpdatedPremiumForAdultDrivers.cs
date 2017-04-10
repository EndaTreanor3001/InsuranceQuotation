using System;
using AppliedSystems.Dtos;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Transforms
{
    public class DriversAndPremiumToUpdatedPremiumForAdultDrivers : ITransform<DriverAndPremium, double>
    {
        private readonly int _startingAdultAge;
        private readonly int _endingAdultAge;
        private readonly IProvide<DateTime> _todayProvider;
        private readonly double _decreasePremiumValue;

        public DriversAndPremiumToUpdatedPremiumForAdultDrivers(int startingAdultAge, int endingAdultAge, [NotNull] IProvide<DateTime> todayProvider, double decreasePremiumValue)
        {
            if (todayProvider == null) throw new ArgumentNullException(nameof(todayProvider));

            _startingAdultAge = startingAdultAge;
            _endingAdultAge = endingAdultAge;
            _todayProvider = todayProvider;
            _decreasePremiumValue = decreasePremiumValue;
        }

        public double Transform([NotNull] DriverAndPremium driverAndPremium)
        {
            if (driverAndPremium == null) throw new ArgumentNullException(nameof(driverAndPremium));

            var driver = driverAndPremium.Driver;
            var premium = driverAndPremium.Premium;

                var today = _todayProvider.Get();
                if (driver.DateOfBirth < today.AddYears(-_startingAdultAge) && driver.DateOfBirth > today.AddYears(-_endingAdultAge))
                {
                    premium -= premium * _decreasePremiumValue;
                }
            return premium;
        }
    }
}
