using System;
using Cysharp.Threading.Tasks;
using DamageSystem;
using DG.Tweening;
using Game.Characters;
using UnityEngine;
using Animation = Game.Characters.Animation;

namespace Game.RoadSystem
{
    public class TrafficCar : MonoBehaviour
    {
        [SerializeField] private int speed;
        private int indexCounter = -1;
        private Vector3[] points;
        private Vector3 CurrentPoint;
        private Vector3 NextPoint;
        private Tween moveTween;

        private Damager Damager;

        private void Awake()
        {
            Damager = GetComponent<Damager>();
        }

        public void Initialize(Vector3[] points)
        {
            this.points = points;
            transform.position = this.points[0];
            transform.LookAt(NextPoint);

            StartBehaviour();
        }

        private void OnEnable()
        {
            Damager.OnHit += OnHit;
        }

        private void OnDisable()
        {
            Damager.OnHit -= OnHit;
        }

        private async void OnHit(Collider collider)
        {
            Damager.enabled = false;
            ControlTagetBehaviour(collider);
            sequence.Pause();
            await UniTask.Delay(3000);
            sequence.Play();
            Damager.enabled = true;
        }

        private async void ControlTagetBehaviour(Collider collider)
        {
            if (!collider.TryGetComponent(out CharacterBase character)) return;
            character.enabled = false;

            var rb = character.Rigidbody;
            rb.linearVelocity = Vector3.zero;
            var direction = (collider.transform.position - transform.position).normalized;
            rb.AddForce(direction * 5, ForceMode.Impulse);

            character.AnimationController.Animator.Play(Animator.StringToHash("Death"));
            await UniTask.Delay(1000);
            if (character.characterStats.CurrentHealth <= 0) return;
            character.AnimationController.Animator.SetTrigger(Animator.StringToHash("wakeUp"));
            await UniTask.Delay(200);
            character.enabled = true;
        }


        private void StartBehaviour()
        {
            StartSequence();
        }

        private Tween UpdateRotation()
        {
            return transform.DOLookAt(NextPoint, 0.3f).SetEase(Ease.InOutQuad);
        }


        public Tween UpdatePosition()
        {
            indexCounter++;
            var count = points.Length;
            CurrentPoint = points[indexCounter % count];
            NextPoint = points[(indexCounter + 1) % count];

            var distance = Vector3.Distance(CurrentPoint, NextPoint);
            var duration = distance / speed;
            return transform.DOMove(NextPoint, duration)
                .SetEase(Ease.Linear);
        }

        private Sequence sequence;

        public void StartSequence()
        {
            sequence = DOTween.Sequence();
            for (var i = 0; i < points.Length; i++)
            {
                sequence.Append(UpdatePosition());
                sequence.Join(UpdateRotation());
            }

            sequence.SetLoops(-1, LoopType.Restart);
        }
    }
}