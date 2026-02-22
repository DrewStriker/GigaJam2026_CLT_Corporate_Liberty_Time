using System.Reflection;
using UnityEngine;

namespace Game.Core
{
    public class ComponentExtensions : MonoBehaviour
    {

        public static T CopyComponent<T>(T originalComponent, GameObject destination) where T : Component
        {
            T copy = destination.AddComponent<T>();
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(originalComponent));
            }

            return copy;
        }
    }
}
