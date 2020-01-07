using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DorllyService.Common
{
    /// <summary>
    /// Comparer the two instance with same class is equality or not
    /// The class must have a property named "Id"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelDistinct<T> : IEqualityComparer<T> where T : class, new()
    {

        private string _compareField;

        public ModelDistinct()
        {
        }

        public ModelDistinct(string compareField)
        {
            _compareField = compareField;
        }

        public bool Equals([AllowNull] T x, [AllowNull] T y)
        {
            var compareField = string.IsNullOrEmpty(_compareField) ? "Id" : _compareField;
            return (dynamic)typeof(T).GetProperty(compareField).GetValue(x) == (dynamic)typeof(T).GetProperty(compareField).GetValue(y);
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
