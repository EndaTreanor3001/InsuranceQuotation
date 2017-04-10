using System;
using System.Collections.Generic;
using System.Linq;
using AppliedSystems.Dtos;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Transforms
{
    public class RejectionMessageAndSuccessBasedOnADriverHavingMoreThanTwoClaimsTransform : ITransform<IEnumerable<IDriver>, RejectionMessageAndSuccess>
    {
        public RejectionMessageAndSuccess Transform([NotNull] IEnumerable<IDriver> drivers)
        {
            if (drivers == null) throw new ArgumentNullException(nameof(drivers));

            var nameOfDriverWithMoreThanTwoClaims = drivers.FirstOrDefault(o => o.Claims.Count() > 2)?.Name;

            if(!string.IsNullOrWhiteSpace(nameOfDriverWithMoreThanTwoClaims)) return new RejectionMessageAndSuccess($"Driver has more than 2 claims { nameOfDriverWithMoreThanTwoClaims}", false);

            return new RejectionMessageAndSuccess(string.Empty, true);
        }
    }
}