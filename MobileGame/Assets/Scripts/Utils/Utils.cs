using System;
using UnityEngine;

namespace MobileGame
{
    public static class Utils
    {
        public static float ClampAround(this float value, float min, float max)
        {
            if (value < min || value > max)
            {
                var l1 = Mathf.Abs(Mathf.Min(min - value, 360 - (min - value)));
                var l2 = Mathf.Abs(Mathf.Min(max - value, 360 - (max - value)));
                if (l1 < l2) value = min; else value = max;
            }
            return value;
        }
        static public float SqrDist(Vector3 v1, Vector3 v2)
        {
            return (v1 - v2).sqrMagnitude;
        }
        public static Vector3 Change(this Vector3 org, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x == null ? org.x : (float)x, y == null ? org.y : (float)y, z == null ? org.z : (float)z);
        }

        public static Vector2 Change(this Vector2 org, object x = null, object y = null)
        {
            return new Vector2(x == null ? org.x : (float)x, y == null ? org.y : (float)y);
        }
    }
}