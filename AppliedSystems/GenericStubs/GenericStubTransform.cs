using System;
using System.Collections.Generic;
using System.Linq;
using AppliedSystems.Interfaces;

namespace AppliedSystems.GenericStubs
{
    public class GenericStubTransform<T, TU> : ITransform<T, TU>
    {
        private readonly IEnumerator<Func<T,TU>> _returnValues;
        public GenericStubTransform()
        {

        }

        public GenericStubTransform(TU returnValueFuncs)
        {
            _returnValues = new[] {returnValueFuncs}.Select(o => new Func<T, TU>(a => o)).GetEnumerator();
        }

        public GenericStubTransform(IEnumerable<TU> returnValues)
        {
            _returnValues = returnValues.Select(o => new Func<T, TU>(a => o)).GetEnumerator();
        }

        public GenericStubTransform(IEnumerable<Func<TU>> returnValueFuncs )
        {
            _returnValues = returnValueFuncs.Select(o => new Func<T, TU>(a => o())).GetEnumerator();
        }

        public GenericStubTransform(params TU[]  returnValues)
        {
            _returnValues = returnValues.Select(o => new Func<T, TU>(a => o)).GetEnumerator();
        }

        public IList<T> ValuesSupplied { get; } = new List<T>();

        public TU Transform(T occurence)
        {
            _returnValues.MoveNext();
            ValuesSupplied.Add(occurence);
            return _returnValues.Current(occurence);
        }
    }
}
