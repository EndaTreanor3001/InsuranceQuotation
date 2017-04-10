using System;
using System.Collections.Generic;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Dtos
{
    public class DriverAndPremium
    {
        public DriverAndPremium([NotNull] IDriver driver, double premium)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));

            Driver = driver;
            Premium = premium;
        }

        [NotNull]
        public IDriver Driver { get; }

        
        public double Premium { get; }

        public override string ToString()
        {
            return $"{nameof(Driver)}: {Driver}, {nameof(Premium)}: {Premium}";
        }
    }
}
