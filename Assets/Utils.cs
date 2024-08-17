using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static Vector3 VecEaseOutInSine(Vector3 a, Vector3 b, float t)
    {
        return new Vector3(EaseOutInSine(a.x, b.x, t), EaseOutInSine(a.y, b.y, t), EaseOutInSine(a.z, b.z, t));
    }

    public static float EaseOutInSine(float a, float b, float t)
    {
        return AnimationCurve.EaseInOut(0, a, 1, b).Evaluate(t);
    }
}
