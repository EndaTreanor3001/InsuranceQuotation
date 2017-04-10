using System.Collections.Generic;
using AppliedSystems.Interfaces;

namespace AppliedSystems.GenericStubs
{
    public class GenericStubDispatch<T> : IDispatch<T>
    {
        public IList<T> ValuesSupplied { get; } = new List<T>();

        public int HitCount { get; private set; }

        public void Dispatch(T occurence)
        {
            ValuesSupplied.Add(occurence);
            HitCount++;
        }
    }
}
