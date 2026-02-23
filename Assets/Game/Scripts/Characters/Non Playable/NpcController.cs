using System;
using System.Collections.Generic;
using Game.Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Characters
{
    public class NpcController : EnemyController
    {
        [SerializeField] private NpcMeshCatalogSO npcMeshCatalog;

        public event Action<NpcDecitionType> Decision;
        private bool canDamagePlayer;

        public List<string> IdleAnimations = new()
        {
            "Idle",
            "Idle2",
            "Talk1",
            "Talk2",
            "Panic_idle"
        };

        protected void Start()
        {
            Rigidbody.isKinematic = true;
            //characterStats.HealthChanged += OnDamage;
            characterStats.ArmorChanged += OnArmorDamage;
            behaviorAgent.SetVariableValue("NpcController", this);
            Renderers[0].GetComponent<MeshFilter>().mesh = npcMeshCatalog.GetRandomHead();
            Renderers[1].GetComponent<SkinnedMeshRenderer>().sharedMesh = npcMeshCatalog.getRandomBody();


            var randomIldeRash =
                Animator.StringToHash(IdleAnimations[Random.Range(0, IdleAnimations.Count)]);
            AnimationController.Animator.speed = Random.Range(0.8f, 1.2f);
            AnimationController.Animator.Play(randomIldeRash);
            transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));
        }


        protected override void OnCollisionEnter(Collision other)
        {
            if (canDamagePlayer) base.OnCollisionEnter(other);
        }

        // Mudar para um evento de Armor Break ao invés de apenas OnDamage, talvez?
        private void OnArmorDamage(int obj)
        {
            NavMeshAgent.enabled = false;
            var randDecision = EnumExtensions.GetRandomEnum<NpcDecitionType>();
            behaviorAgent.SetVariableValue("decision", randDecision);
            Decision?.Invoke(randDecision);
            //characterStats.HealthChanged -= OnArmorDamage;
            characterStats.ArmorChanged -= OnArmorDamage;
            NavMeshAgent.enabled = true;
        }

        public void EnableDamage()
        {
            canDamagePlayer = true;
        }
    }
}