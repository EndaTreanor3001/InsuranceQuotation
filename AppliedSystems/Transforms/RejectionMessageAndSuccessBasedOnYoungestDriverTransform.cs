using System;
using System.Collections.Generic;
using System.Linq;
using AppliedSystems.Dtos;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Transforms
{
    public class RejectionMessageAndSuccessBasedOnYoungestDriverTransform : ITransform<IEnumerable<IDriver>, RejectionMessageAndSuccess>
    {
        private readonly int _minimumAge;
        private readonly IProvide<DateTime> _todayProvider;

        public RejectionMessageAndSuccessBasedOnYoungestDriverTransform(int minimumAge, [NotNull] IProvide<DateTime> todayProvider)
        {
            if (todayProvider == null) throw new ArgumentNullException(nameof(todayProvider));

            _minimumAge = minimumAge;
            _todayProvider = todayProvider;
        }

        public RejectionMessageAndSuccess Transform([NotNull] IEnumerable<IDriver> drivers)
        {
            if (drivers == null) throw new ArgumentNullException(nameof(drivers));

            var driversArray = drivers.ToArray();
            if(!driversArray.Any()) return new RejectionMessageAndSuccess(string.Empty, true);

            var youngestDriver = driversArray.OrderByDescending(o => o.DateOfBirth).First();

            var youngestDriverIsOlderThanMinimumAge = youngestDriver.DateOfBirth <= _todayProvider.Get().AddYears(-_minimumAge);

            if(!youngestDriverIsOlderThanMinimumAge) return new RejectionMessageAndSuccess($"Age of Youngest Driver {youngestDriver.Name}", false);

            return new RejectionMessageAndSuccess(string.Empty, true);
        }
    }
}