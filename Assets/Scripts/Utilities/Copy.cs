using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copy
{
    public static T Component<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        var dst = destination.GetComponent(type) as T;
        if (dst == null) dst = destination.AddComponent(type) as T;
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            if (field.IsStatic) continue;
            field.SetValue(dst, field.GetValue(original));
        }
        var props = type.GetProperties();
        foreach (var prop in props)
        {
            if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
            prop.SetValue(dst, prop.GetValue(original, null), null);
        }
        return dst as T;
    }
}
