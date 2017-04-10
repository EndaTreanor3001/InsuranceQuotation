using System;
using System.Collections.Generic;

namespace AppliedSystems.Interfaces
{
    public interface IPolicy
    {
        DateTime StartDate { get; }

        IEnumerable<IDriver> Drivers { get; }

    }
}
