using System;
using System.Collections.Generic;
using System.Linq;
using AppliedSystems.Dtos;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Transforms
{
    public class RejectionMessageAndSuccessBasedOnOldestDriverTransform :  ITransform<IEnumerable<IDriver>, RejectionMessageAndSuccess>
    {
        private readonly int _maximumAge;
        private readonly IProvide<DateTime> _todayProvider;

        public RejectionMessageAndSuccessBasedOnOldestDriverTransform(int maximumAge, [NotNull] IProvide<DateTime> todayProvider)
        {
            if (todayProvider == null) throw new ArgumentNullException(nameof(todayProvider));

            _maximumAge = maximumAge;
            _todayProvider = todayProvider;
        }

        public RejectionMessageAndSuccess Transform([NotNull] IEnumerable<IDriver> drivers)
        {
            if (drivers == null) throw new ArgumentNullException(nameof(drivers));

            var driversArray = drivers.ToArray();
            if(!driversArray.Any()) return new RejectionMessageAndSuccess(string.Empty, true);
            
            var oldestDriver = driversArray.OrderBy(o => o.DateOfBirth).First();

            var oldestDriverIsOlderThanMaximumAge = oldestDriver.DateOfBirth < _todayProvider.Get().AddYears(-_maximumAge);

            if(oldestDriverIsOlderThanMaximumAge) return new RejectionMessageAndSuccess($"Age of Oldest Driver {oldestDriver.Name}", false);

            return new RejectionMessageAndSuccess(string.Empty, true);

        }
    }
}
