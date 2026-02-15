using System;
using DG.Tweening;
using Game.Core;
using Game.Scripts.BuffSystem;
using UnityEngine;

namespace Game.CollectableSystem
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class CollectableObject<T> : MonoBehaviour, ICollectable<T> where T : Enum
    {
        private const float floatingHight = 0.5f;
        private const float floatingDuration = 1;
        private Tween colorTween;
        private Tween moveTween;
        private Renderer renderer;

        private void Awake()
        {
            renderer = GetComponentInChildren<Renderer>();
        }

        private void OnEnable()
        {
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

        [field: SerializeField] public BuffDataSO BuffData { get; private set; }
        public Transform Transform => transform;


        public virtual void Collect(ICollector<T> collector)
        {
            collector.Collect(this);
            StopEffect();
        }


        private void StartEffect()
        {
            StopEffect();
            moveTween = transform.DOMoveY(transform.position.y + floatingHight, floatingDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
            colorTween = renderer.DoColor(
                    ShaderProperties.OverlayColor, new Color(1, 1, 1, 0.3f),
                    floatingDuration / 3)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void StopEffect()
        {
            moveTween?.Kill();
            colorTween?.Kill();
            renderer.DoColor(ShaderProperties.OverlayColor, new Color(1, 1, 1, 0), 0);
        }
    }
}