using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfFrontEnd.ViewModels
{
    public static class AsObservableCollectionExtensionMethod
    {
        public static ObservableCollection<T> AsObservableCollection<T>(this IEnumerable<T> enumnerable)
        {
            var gh = new ObservableCollection<T>();
            foreach (var item in enumnerable)
            {
                gh.Add(item);
            }
            return gh;
        }

        
    }
}