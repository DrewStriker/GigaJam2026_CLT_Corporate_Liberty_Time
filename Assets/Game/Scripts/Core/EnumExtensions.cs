using System;

namespace Game.Scripts.Core
{
    public static class EnumExtensions
    {
        private static Random rnd = new();

        public static T GetRandomEnum<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(rnd.Next(values.Length));
        }

        public static int Size<T>()
        {
            return Enum.GetValues(typeof(T)).Length;
        }
    }
}