using System;
using AppliedSystems.Interfaces;

namespace AppliedSystems.Dtos
{
    public class Claim : IClaim
    {
        public Claim(DateTime dateOfClaim)
        {
            DateOfClaim = dateOfClaim;
        }

        public DateTime DateOfClaim { get; }

        public override string ToString()
        {
            return $"{nameof(DateOfClaim)}: {DateOfClaim}";
        }
    }
}
