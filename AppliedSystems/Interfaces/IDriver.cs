using System;
using System.Collections.Generic;

namespace AppliedSystems.Interfaces
{
    public interface IDriver
    {
        string Name { get;  }

        string Occupation { get; }

        DateTime DateOfBirth { get; }

        IEnumerable<IClaim> Claims { get; }
    }
}