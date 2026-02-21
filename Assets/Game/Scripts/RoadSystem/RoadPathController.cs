using System;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.RoadSystem
{
    [RequireComponent(typeof(SplineContainer))]
    public class RoadPathController : MonoBehaviour
    {
        [SerializeField] [Range(0, 10)] private float carSpeed = 10;
        [SerializeField] private GameObject[] CarPrefabs;
        private SplineContainer splineContainer;
        private ITrafficCar[] trafficCars;

        private void Awake()
        {
            splineContainer = GetComponent<SplineContainer>();
            trafficCars = new ITrafficCar[splineContainer.Splines.Count];
            for (var i = 0; i < splineContainer.Splines.Count; i++)
            {
                var spline = splineContainer.Splines[i];
                var carInstance = Instantiate(
                    CarPrefabs[UnityEngine.Random.Range(0, CarPrefabs.Length)], transform);
                trafficCars[i] = new TrafficCar(carInstance, carSpeed, splineContainer, spline);
            }
        }

        [ContextMenu("Linearise Knots")]
        public void LineariseKnots()
        {
            if (splineContainer == null)
                splineContainer = GetComponent<SplineContainer>();


            foreach (var spline in splineContainer.Splines)
                for (var i = 0; i < spline.Count; i++)
                    spline.SetTangentMode(i, TangentMode.Linear);
        }
    }
}