using Game.Scripts.Core;
using UnityEngine;
using UnityEngine.Splines;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.RoadSystem
{
    [RequireComponent(typeof(SplineContainer))]
    public class RoadPathController : MonoBehaviour
    {
        [SerializeField] private int carSpeed = 10;
        [SerializeField] private TrafficCar[] CarPrefabs;
        private SplineContainer splineContainer;
        [Inject] private PlaceholderFactory<CarType, TrafficCar> carFactory;

        private void Awake()
        {
            splineContainer = GetComponent<SplineContainer>();
            for (var i = 0; i < splineContainer.Splines.Count; i++)
            {
                var spline = splineContainer.Splines[i];
                var points = GetSplinePointsLocal(spline);
                var prefab = CarPrefabs[Random.Range(0, CarPrefabs.Length)];
                var randomCar = EnumExtensions.GetRandomEnum<CarType>();
                var carInstance = carFactory.Create(randomCar);
                carInstance.Initialize(points);
            }
        }

        private Vector3[] GetSplinePointsLocal(Spline spline)
        {
            var points = new Vector3[spline.Count];

            for (var i = 0; i < spline.Count; i++)
            {
                var position = spline[i].Position;
                points[i] = splineContainer.transform.TransformPoint(position);
            }

            return points;
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