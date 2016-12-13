using UnityEngine;
using System.Collections;
namespace Alta.Plugin
{
    public static class VectorExtension
    {
        public static void setZ(this Vector3 vec, float z)
        {
             vec= new Vector3(vec.x, vec.y, z);
        }
        public static void setY(this Vector3 vec, float y)
        {
            vec= new Vector3(vec.x, y, vec.z);
        }
        public static void setX(this Vector2 v, float x)
        {
            v = new Vector2(x, v.y); 
        }
        public static void setY(this Vector2 v, float y)
        {
            v = new Vector2(v.x, y);
        }
        public static Vector3 to3(this Vector2 vec, float z)
        {
            return new Vector3(vec.x, vec.y, z);
        }
    }
}
