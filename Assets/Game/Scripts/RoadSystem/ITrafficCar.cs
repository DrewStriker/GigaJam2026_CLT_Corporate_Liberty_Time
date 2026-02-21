using UnityEngine;
using UnityEngine.Splines;
using Zenject;

namespace Game.RoadSystem
{
    public interface ITrafficCar
    {
        public GameObject CarInstance { get; }
        public Spline spline { get; }
    }
}