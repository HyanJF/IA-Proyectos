using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    private Dictionary<string, object> data = new Dictionary<string, object>();

    public void SetValue(string key, object value)
    {
        if (data.ContainsKey(key)) data[key] = value;
        else data.Add(key, value);
    }

    public T GetValue<T>(string key)
    {
        if (data.ContainsKey(key) && data[key] is T) return (T)data[key];
        return default;
    }

    public bool HasValue(string key) => data.ContainsKey(key);

    public void Remove(string key)
    {
        if (data.ContainsKey(key)) data.Remove(key);
    }
}
