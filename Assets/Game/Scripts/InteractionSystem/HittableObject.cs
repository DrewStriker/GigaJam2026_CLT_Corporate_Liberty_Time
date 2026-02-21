using System;
using System.Runtime.CompilerServices;
using Game.Core;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game.InteractionSystem
{
    public class HittableObject : InteractableObject, IHittable
    {
        [Inject] private ITarget player;
        [SerializeField] private UnityEvent OnHit;
        private bool hasBeenHit;
        public bool IsDamageActive { get; }

        public override bool IsDamageValid()
        {
            return IsMoving && !hasBeenHit;
        }


        public void TakeDamage(DamageData damageData)
        {
            if (hasBeenHit) return;
            Rigidbody.isKinematic = false;
            var inverseHitDirection = -(transform.position - damageData.AttackerPosition).normalized;
            //  inverseHitDirection.y = 0;
            transform.forward = player.Transform.forward;
            Interact();
            OnHit?.Invoke();
            hasBeenHit = true;
        }


        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (IsMoving) TryDamageEnemy(other);
        }

        private void Update()
        {
            if (!IsMoving && hasBeenHit)
                gameObject.SetActive(false);
        }
    }
}