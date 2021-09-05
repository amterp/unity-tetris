using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictUtil
{
    public static V GetValueOrDefault<K, V>(this IDictionary<K, V> dict, K key, V defaultValue = default(V))
    {
        V value;
        return dict.TryGetValue(key, out value) ? value : defaultValue;
    }
}
