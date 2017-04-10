using System;
using System.Collections.Generic;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Dtos.Stubs
{
    public class DriverStub : IDriver
    {
        public DriverStub([NotNull] string name, [NotNull] string occupation, DateTime dateOfBirth, [NotNull] IEnumerable<IClaim> claims)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (occupation == null) throw new ArgumentNullException(nameof(occupation));
            if (claims == null) throw new ArgumentNullException(nameof(claims));

            Name = name;
            Occupation = occupation;
            DateOfBirth = dateOfBirth;
            Claims = claims;
        }

        public string Name { get; set; }
        public string Occupation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<IClaim> Claims { get; set; }
    }
}
