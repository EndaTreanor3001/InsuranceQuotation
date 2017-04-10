using System;
using System.Collections.Generic;
using System.Linq;
using AppliedSystems.Interfaces;

namespace AppliedSystems.GenericStubs
{
    public class GenericStubProvider<T> : IProvide<T>
    {
        private readonly IEnumerator<Func<T>> _returnValues;
        private readonly IList<T> _valuesSupplied = new List<T>();

        public GenericStubProvider()
        {
        }

        public GenericStubProvider(IEnumerable<T> returnValues )
        {
            _returnValues = returnValues.Select(o => new Func<T>(() => o)).GetEnumerator();
        }

        public GenericStubProvider(T returnValue )
        {
            var funcs = new[] { new Func<T>(() => returnValue)};
            _returnValues = funcs.Select(o => o).GetEnumerator();
        }

        public GenericStubProvider(IEnumerable<Func<T>> returnValueFuncs)
        {
            _returnValues = returnValueFuncs.GetEnumerator();
        }

        public IList<T> ValuesSupplied => _valuesSupplied;

        public T Get()
        {
            _returnValues.MoveNext();
            _valuesSupplied.Add(_returnValues.Current());
            return _returnValues.Current();
        }
    }
}
