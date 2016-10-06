using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ScjnUtilities
{
    public static class Extensiones
    {

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            var col = new ObservableCollection<T>();
            foreach (var cur in enumerable)
            {
                col.Add(cur);
            }
            return col;
        }

    }
}
