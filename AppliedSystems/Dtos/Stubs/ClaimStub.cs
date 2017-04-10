using System;
using AppliedSystems.Interfaces;

namespace AppliedSystems.Dtos.Stubs
{
    public class ClaimStub : IClaim
    {
        public ClaimStub(DateTime dateOfClaim)
        {
            DateOfClaim = dateOfClaim;
        }

        public DateTime DateOfClaim { get; set; }
    }
}
