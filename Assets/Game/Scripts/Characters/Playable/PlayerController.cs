using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DamageSystem;
using DG.Tweening;
using Game.CollectableSystem;
using Game.Core;
using Game.Core.SimplePool.SfxPool;
using Game.Core.SimplePool.VfxPool;
using Game.Input;
using Game.InteractionSystem;
using Game.ItemSystem;
using Game.WeaponSystem;
using UnityEngine;
using Zenject;

namespace Game.Characters
{
    public class PlayerController : CharacterBase, IPlayableCharacter
    {
        [SerializeField] private Transform handTransform;

        [Inject] public PlayerInputController InputController { get; private set; }
        [Inject] public SfxPoolFacade SfxPoolFacade { get; private set; }
        public PlayableCharacterMovementController MovementController { get; private set; }
        public PlayerStateMachine StateMachine { get; private set; }
        public IDamager Damager { get; private set; }
        public ICollectable<WeaponType> WeaponEquipped { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Damager = GetComponentInChildren<IDamager>();
            MovementController =
                new PlayableCharacterMovementController(GetComponent<Rigidbody>(), characterStats, InputController);
            StateMachine = new PlayerStateMachine(this);
        }

        private void Update()
        {
            StateMachine.Update();

            if (grabbedObject == null) return;
            if (InputController.Interact.WasPerformedThisFrame()) grabbedObject.Grab(transform);

            if (InputController.Interact.WasReleasedThisFrame()) grabbedObject.Release();
        }

        //TODO: Refact Latter
        private IGrabbable grabbedObject;


        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.Grabbable)) return;
            if (!other.TryGetComponent(out IGrabbable interactable)) return;
            if (grabbedObject == null)
            {
                grabbedObject = interactable;
                return;
            }

            if (!IsNearThanActual(other.transform)) return;
            grabbedObject = interactable;
        }

        private bool IsNearThanActual(Transform other)
        {
            var newDistance = Vector3.Distance(transform.position, other.transform.position);
            var previousObjDistance = Vector3.Distance(transform.position, grabbedObject.Transform.position);
            return newDistance < previousObjDistance;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Tags.Grabbable)) return;
            if (other.TryGetComponent(out IInteractable interactable))
                if (interactable == grabbedObject)
                    grabbedObject = null;
        }


        private void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }


        public void UpdateBaseAnimation()
        {
            var moveDirection = Mathf.Abs(InputController.Direction.sqrMagnitude);
            AnimationController.SetFloat(AnimationParameter.VelocityH, moveDirection);
        }

        public override void TakeDamage(DamageData damageData)
        {
            base.TakeDamage(damageData);
            SfxPoolFacade.Play(SfxType.Hurt, transform.position, 1, true);
            Knockback(damageData.AttackerPosition);
        }

        public void Collect(ICollectable<WeaponType> item)
        {
            if (WeaponEquipped != null) WeaponEquipped.UnCollect();
            WeaponEquipped = item;
            item.SetParent(handTransform);
            item.BuffData.ApplyBuffTo(characterStats);
        }

        public async void Collect(ICollectable<ItemType> item)
        {
            item.BuffData.ApplyBuffTo(characterStats);
            //TODO: provisory!!!! Refact latter
            CoffeePowerUpBehaviour(item);
        }

        //TODO: provisory!!!! Refact latter
        private async void CoffeePowerUpBehaviour(ICollectable<ItemType> item)
        {
            if (item.Type == ItemType.Coffee)
            {
                IsDamageActive = false;
                transform.DOScale(3, 1)
                    .SetEase(Ease.OutQuad);
                Damager.Bounds = new Bounds(new Vector3(0, 2, 0), new Vector3(2, 4, 2) * 2);
                var elapsedTime = 0f;
                try
                {
                    while (elapsedTime < item.BuffData.Duration)
                    {
                        Damager.DoDamage(characterStats.Damage);
                        await UniTask.Yield(PlayerLoopTiming.Update);
                        elapsedTime += Time.deltaTime;
                    }
                }
                catch (Exception e)
                {
                }

                IsDamageActive = true;

                Damager.Bounds = new Bounds(Vector3.up + Vector3.forward, Vector3.one * 2);
                transform.DOScale(1, 0.5f).SetEase(Ease.OutQuad);
            }
        }

        private void Knockback(Vector3 position)
        {
            var direction = (transform.position - position).normalized;
            direction.y = 0;
            Rigidbody.AddForce(direction * 7, ForceMode.Impulse);
        }

        public void Attack()
        {
            Damager.DoDamage(characterStats.Damage);
        }
    }
}