using System;
using System.Linq;
using AppliedSystems.Dtos;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Transforms
{
    public class DriversAndPremiumToUpdatedPremiumBasedOnOccupationTransform : ITransform<DriverAndPremium, double>
    {
        private readonly ITransform<DriverAndPremium, double> _driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform;
        private readonly ITransform<DriverAndPremium, double> _driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform;

        public DriversAndPremiumToUpdatedPremiumBasedOnOccupationTransform([NotNull] ITransform<DriverAndPremium, double> driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform,
                                                                           [NotNull] ITransform<DriverAndPremium, double> driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform)
        {
            if (driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform == null) throw new ArgumentNullException(nameof(driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform));
            if (driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform == null) throw new ArgumentNullException(nameof(driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform));

            _driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform = driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform;
            _driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform = driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform;
        }

        public double Transform([NotNull] DriverAndPremium driverAndPremium)
        {
            if (driverAndPremium == null) throw new ArgumentNullException(nameof(driverAndPremium));

            var driver = driverAndPremium.Driver;

            var potentiallyIncreasedPremium = _driversAndPremiumToIncreasedPremiumBasedOnOccupationTransform.Transform(driverAndPremium);
            return _driversAndPremiumToDecreasedPremiumBasedOnOccupationTransform.Transform(new DriverAndPremium(driver, potentiallyIncreasedPremium));
        }
    }
}
