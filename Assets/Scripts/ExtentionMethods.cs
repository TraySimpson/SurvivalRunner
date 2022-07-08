using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static Vector3 ToXZVector3(this Vector2 v2)
    {
        return new Vector3(v2.x, 0, v2.y);
    }
}
