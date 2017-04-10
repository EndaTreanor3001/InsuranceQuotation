using System;
using System.Collections.Generic;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Dtos.Stubs
{
    public class PolicyStub : IPolicy
    {
        public PolicyStub(DateTime startDate, [NotNull] IEnumerable<IDriver> drivers)
        {
            if (drivers == null) throw new ArgumentNullException(nameof(drivers));

            StartDate = startDate;
            Drivers = drivers;
        }

        public DateTime StartDate { get; set; }

        public IEnumerable<IDriver> Drivers { get; set; }
    }
}
