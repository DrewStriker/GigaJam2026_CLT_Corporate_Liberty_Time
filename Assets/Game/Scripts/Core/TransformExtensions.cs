using UnityEngine;


namespace Game.core
{
    public static class TransformExtensions
    {
        public static float DistanceTo(this Transform transform, Transform target)
        {
            return DirectionTo(transform, target).magnitude;
        }

        public static Vector3 DirectionTo(this Transform transform, Transform target)
        {
            return target.position - transform.position;
        }

        public static void RandomRotationY(this Transform transform, float min, float max)
        {
            transform.rotation = Quaternion.Euler(0, Random.Range(min, max), 0);
        }
    }
}