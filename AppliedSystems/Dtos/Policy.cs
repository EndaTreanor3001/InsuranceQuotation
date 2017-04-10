using System;
using System.Collections.Generic;
using System.Linq;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Dtos
{
    public class Policy : IPolicy
    {
        public Policy(DateTime startDate, [NotNull] IEnumerable<IDriver> drivers)
        {
            if (drivers == null) throw new ArgumentNullException(nameof(drivers));

            StartDate = startDate;
            Drivers = drivers;
        }

        public DateTime StartDate { get; }

        [NotNull]
        public IEnumerable<IDriver> Drivers { get;  }

        public override string ToString()
        {
            return $"{nameof(StartDate)}: {StartDate}, {nameof(Drivers)}: {string.Join("\n", Drivers.Select(o => o.ToString()))}";
        }
    }
}
