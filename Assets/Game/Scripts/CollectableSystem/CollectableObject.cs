using System;
using System.Runtime.CompilerServices;
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
        [field: SerializeField] public T Type { get; private set; }
        private const float floatingHight = 0.5f;
        private const float floatingDuration = 1;
        private Tween colorTween;
        private Tween moveTween;
        private new Renderer renderer;
        public ParticleSystem CollectEffect { get; private set; }
        [field: SerializeField] public BuffDataSO BuffData { get; private set; }
        public Transform Transform => transform;
        public event Action<ICollectable<T>> OnCollected;

        private void Awake()
        {
            renderer = GetComponentInChildren<Renderer>();
            CollectEffect = GetComponentInChildren<ParticleSystem>();
        }

        private void OnEnable()
        {
            StartEffect();
            transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
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
            OnCollected?.Invoke(this);
        }

        public void UnCollect()
        {
            GetComponent<Collider>().enabled = false;

            transform.SetParent(null);
            renderer.DoColor(ShaderProperties.OverlayColor, Color.black, 0);
            transform.DOLocalRotate(Vector3.one * 360, 0.2f)
                .SetLoops(-1, LoopType.Incremental);
            transform.DOLocalMoveY(10, 0.6f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => { gameObject.SetActive(false); });
            // transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            // StartEffect();
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
            moveTween?.Kill();
            colorTween?.Kill();
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