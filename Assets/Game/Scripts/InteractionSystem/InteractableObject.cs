using System;
using DamageSystem;
using DG.Tweening;
using Game.Core;
using Game.Input;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.InteractionSystem
{
    
    //Do Damage to Enemies (followers and NPCs) when, behaviour is active
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private InteractionBehaviourSO behaviour;
        private DamageData damageData = new();
        private bool isMoving = false;
        private Tween colorTween;
        private NavMeshObstacle navMeshObstacle;
        public new Renderer Renderer { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Transform Transform => transform;


        private void Awake()
        {
            navMeshObstacle = GetComponent<NavMeshObstacle>();
            Renderer = GetComponentInChildren<Renderer>();
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if(isMoving && Rigidbody.IsSleeping())
            {
                isMoving = false;
                navMeshObstacle.enabled = true;
            }
        }
        


        private void OnTriggerEnter(Collider other)
        {
            TryDamageEnemy(other);
            if (!other.gameObject.CompareTag(Tags.Player)) return;
            Interact();
            StartEffect();
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag(Tags.Player)) return;
            StopEffect();
        }

        public virtual void Interact()
        {
            behaviour.Execute(this);
            isMoving = true;
            navMeshObstacle.enabled = false;

        }
        

        private void StartEffect()
        {
            colorTween = Renderer.DoColor(
                ShaderProperties.OverlayColor,
                new Color(1, 1, 1, 0.3f),
                0);

        }

        private void StopEffect()
        {
            colorTween?.Kill();
            Renderer.DoColor(ShaderProperties.OverlayColor,
                new Color(1, 1, 1, 0),
                0);
        }

        private void TryDamageEnemy(Collider other)
        {
            if (!other.CompareTag(Tags.Enemy)) return;
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            damageData.Configure(damage,transform.position);
            damageable.TakeDamage(damageData);
            
            
        }
        
        
    }
}