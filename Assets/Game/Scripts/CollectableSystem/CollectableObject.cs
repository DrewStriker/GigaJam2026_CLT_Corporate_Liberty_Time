using System;
using DG.Tweening;
using Game.core;
using Game.Core;
using Game.Scripts.BuffSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.CollectableSystem
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class CollectableObject<T> : MonoBehaviour, ICollectable<T> where T : Enum
    {
        private const float floatingHight = 0.5f;
        private const float floatingDuration = 1;
        private Tween colorTween;
        private Tween moveTween;
        private new Renderer renderer;
        [field: SerializeField] public BuffDataSO BuffData { get; private set; }
        public Transform Transform => transform;

        private void Awake()
        {
            renderer = GetComponentInChildren<Renderer>();
        }

        private void OnEnable()
        {
            transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            transform.RandomRotationY(0, 360);
            StartEffect();
        }

        private void OnDisable()
        {
            StopEffect();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.Player)) return;
            if (!other.TryGetComponent<ICollector<T>>(out var collector)) return;
            Collect(collector);
        }


        public virtual void Collect(ICollector<T> collector)
        {
            StopEffect();
            collector.Collect(this);
        }


        private void StartEffect()
        {
            StopEffect();
            moveTween = transform.DOMoveY(transform.position.y + floatingHight, floatingDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutQuad);
            colorTween = renderer.DoColor(
                    ShaderProperties.OverlayColor, new Color(1, 1, 1, 0.3f),
                    floatingDuration / 2.2f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }

        private void StopEffect()
        {
            moveTween.Kill(true);
            colorTween.Kill(true);
            transform.localRotation = Quaternion.identity;
            renderer.DoColor(ShaderProperties.OverlayColor, new Color(1, 1, 1, 0), 0);
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
            transform.localPosition = Vector3.zero;
        }
    }
}