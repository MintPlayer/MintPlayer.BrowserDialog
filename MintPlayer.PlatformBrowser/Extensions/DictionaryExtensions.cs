using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MintPlayer.PlatformBrowser.Extensions
{
    internal static class DictionaryExtensions
    {
        internal static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> source)
            => new ReadOnlyDictionary<TKey, TValue>(source);
    }
}
