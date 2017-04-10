using System;
using System.Collections.Generic;
using System.Linq;
using AppliedSystems.Interfaces;
using JetBrains.Annotations;

namespace AppliedSystems.Dtos
{
    public class Driver : IDriver
    {
        public Driver([NotNull] string name, [NotNull] string occupation, DateTime dateOfBirth, [NotNull] IEnumerable<IClaim> claims)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (occupation == null) throw new ArgumentNullException(nameof(occupation));
            if (claims == null) throw new ArgumentNullException(nameof(claims));

            Name = name;
            Occupation = occupation;
            DateOfBirth = dateOfBirth;
            Claims = claims;
        }

        [NotNull]
        public string Name { get; }

        [NotNull]
        public string Occupation { get; }

        public DateTime DateOfBirth { get; }

        [NotNull]
        public IEnumerable<IClaim> Claims { get; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Occupation)}: {Occupation}, {nameof(DateOfBirth)}: {DateOfBirth}, {nameof(Claims)}: {string.Join(",", Claims.Select(o => o.ToString()))}";
        }
    }
}
