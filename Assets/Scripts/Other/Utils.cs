using System;
using UnityEngine;

public static class Utils
{
    public static void NotFound(UnityEngine.Object obj, string objName, string name)
    {
        if (obj == null)
        {
            Debug.LogError("No " + objName + " found on " + name);
            throw new NullReferenceException();
        }
    }

    public static T SafeGetComponent<T>(MonoBehaviour obj) where T : Component
    {
        T comp = obj.GetComponent<T>();
        NotFound(comp, typeof(T).Name, obj.name);
        return comp;
    }

    public static T SafeGetComponentInChildren<T>(MonoBehaviour obj) where T : Component
    {
        T comp = obj.GetComponentInChildren<T>();   
        NotFound(comp, typeof(T).Name, obj.name);
        return comp;
    }
}
