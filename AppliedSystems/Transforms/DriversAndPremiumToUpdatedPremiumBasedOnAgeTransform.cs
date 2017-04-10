using System;
using AppliedSystems.Dtos;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Transforms
{
    public class DriversAndPremiumToUpdatedPremiumBasedOnAgeTransform : ITransform<DriverAndPremium, double>
    {
        private readonly ITransform<DriverAndPremium, double> _driversAndPremiumToUpdatedPremiumForYoungDrivers;
        private readonly ITransform<DriverAndPremium, double> _driversAndPremiumToUpdatedPremiumForAdultDrivers;

        public DriversAndPremiumToUpdatedPremiumBasedOnAgeTransform([NotNull] ITransform<DriverAndPremium, double> driversAndPremiumToUpdatedPremiumForYoungDrivers,
                                                                    [NotNull] ITransform<DriverAndPremium, double> driversAndPremiumToUpdatedPremiumForAdultDrivers)
        {
            if (driversAndPremiumToUpdatedPremiumForYoungDrivers == null) throw new ArgumentNullException(nameof(driversAndPremiumToUpdatedPremiumForYoungDrivers));
            if (driversAndPremiumToUpdatedPremiumForAdultDrivers == null) throw new ArgumentNullException(nameof(driversAndPremiumToUpdatedPremiumForAdultDrivers));

            _driversAndPremiumToUpdatedPremiumForYoungDrivers = driversAndPremiumToUpdatedPremiumForYoungDrivers;
            _driversAndPremiumToUpdatedPremiumForAdultDrivers = driversAndPremiumToUpdatedPremiumForAdultDrivers;
        }

        public double Transform([NotNull] DriverAndPremium driverAndPremium)
        {
            if (driverAndPremium == null) throw new ArgumentNullException(nameof(driverAndPremium));
            var driver = driverAndPremium.Driver;

            var updatedPremiumWhenDriversAreYoung = _driversAndPremiumToUpdatedPremiumForYoungDrivers.Transform(driverAndPremium);

            var driversWithUpdatePremium = new DriverAndPremium(driver, updatedPremiumWhenDriversAreYoung);
            var updatedPremiumWhenDriversAreAdults = _driversAndPremiumToUpdatedPremiumForAdultDrivers.Transform(driversWithUpdatePremium);

            return updatedPremiumWhenDriversAreAdults;
        }
    }
}
