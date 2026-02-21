using System;
using DamageSystem;
using DG.Tweening;
using Game.Core;
using Game.Input;
using Game.Scripts.Core;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.InteractionSystem
{
    //Do Damage to Enemies (followers and NPCs) when, behaviour is active
    public abstract class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private InteractionBehaviourSO behaviour;
        private DamageData damageData = new();
        private Tween colorTween;
        private NavMeshObstacle navMeshObstacle;
        public Renderer Renderer { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        protected bool IsMoving { get; private set; } = false;
        public Transform Transform => transform;
        public abstract bool IsDamageValid();

        private void Awake()
        {
            navMeshObstacle = GetComponent<NavMeshObstacle>();
            Renderer = GetComponentInChildren<Renderer>();
            Rigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void FixedUpdate()
        {
            if (IsMoving && Rigidbody.IsSleeping())
            {
                IsMoving = false;
                navMeshObstacle.enabled = true;
            }
        }


        protected virtual void OnTriggerEnter(Collider other)
        {
            if (IsDamageValid()) TryDamageEnemy(other);
            if (other.gameObject.CompareTag(Tags.Player)) StartEffect();
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsMoving) return;
            if (!other.gameObject.CompareTag(Tags.Player)) return;
            StopEffect();
        }

        public virtual void Interact()
        {
            StopEffect();

            behaviour.Execute(this);
            IsMoving = true;
            navMeshObstacle.enabled = false;
        }


        private void StartEffect()
        {
            colorTween = Renderer.DoColor(
                ShaderProperties.OverlayColor,
                GameColors.InteractionOn,
                0);
        }

        private void StopEffect()
        {
            colorTween?.Kill();
            Renderer.DoColor(ShaderProperties.OverlayColor,
                GameColors.Transparent,
                0);
        }

        protected void TryDamageEnemy(Collider other)
        {
            if (!other.CompareTag(Tags.Enemy)) return;
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            damageData.Configure(damage, transform.position);
            damageable.TakeDamage(damageData);
        }
    }
}