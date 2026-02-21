using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.RoadSystem
{
    public class TrafficCar : ITrafficCar
    {
        private Vector3 position;
        private float speed;
        private int indexCounter = -1;
        private Vector3 CurrentPoint;
        private Vector3 NextPoint;
        private Transform transform => CarInstance.transform;
        public GameObject CarInstance { get; private set; }
        public Spline spline { get; private set; }

        private Vector3 NextDirection => (NextPoint - transform.position).normalized;
        private SplineContainer _splineContainer;

        private Sequence behaviourSequence = DOTween.Sequence();

        public TrafficCar(GameObject carInstance, float carSpeed, SplineContainer splineContainer, Spline spline)
        {
            _splineContainer = splineContainer;
            CarInstance = carInstance;
            speed = carSpeed;
            this.spline = spline;
            transform.position = splineContainer.transform.TransformPoint(spline[0].Position);
            StartBehaviour();
            // StartMoveSequence();
        }


        public Tween UpdatePoints()
        {
            indexCounter++;
            var currentPoint = spline[indexCounter % spline.Count].Position;
            var nextPoint = spline[(indexCounter + 1) % spline.Count].Position;
            CurrentPoint = _splineContainer.transform.TransformPoint(currentPoint);
            NextPoint = _splineContainer.transform.TransformPoint(nextPoint);
            return DOVirtual.DelayedCall(0f, () => { });
        }

        private Tween UpdateRotation()
        {
            return transform.DOLookAt(NextPoint, 0.5f).SetEase(Ease.InOutQuad);
        }

        private void StartBehaviour()
        {
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            UpdatePoints();
            UpdateRotation();
            transform.DOMove(NextPoint, speed)
                .SetSpeedBased(true)
                .SetEase(Ease.InOutQuad).OnComplete(UpdatePosition);
        }
    }
}