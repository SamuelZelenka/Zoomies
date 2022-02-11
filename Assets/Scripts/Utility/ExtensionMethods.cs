using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static float Dot01(Vector3 lhs, Vector3 rhs) //this method is not an extension method
    {
        float angle = Vector3.Angle(lhs, rhs);

        return Mathf.Cos(Mathf.Deg2Rad * angle / 2);
    }

    public static bool ApproximatelyWithin(this float a, float b, float threshold)
    {
        return Mathf.Abs(a - b) <= threshold;
    }
}
