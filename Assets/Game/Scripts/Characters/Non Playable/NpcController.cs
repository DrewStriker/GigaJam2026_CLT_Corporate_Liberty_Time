using System;
using Game.Scripts.Core;
using UnityEngine;

namespace Game.Characters
{
    public class NpcController : EnemyController
    {
        public event Action<NpcDecitionType> Decision;
        private bool canDamagePlayer;

        protected override void Start()
        {
            Rigidbody.isKinematic = true;
            //characterStats.HealthChanged += OnDamage;
            characterStats.ArmorChanged += OnArmorDamage;
            base.Start();
            behaviorAgent.SetVariableValue("NpcController", this);
        }

        protected override void OnCollisionEnter(Collision other)
        {
            if (canDamagePlayer) base.OnCollisionEnter(other);
        }

        // Mudar para um evento de Armor Break ao invés de apenas OnDamage, talvez?
        private void OnArmorDamage(int obj)
        {
            var randDecision = EnumExtensions.GetRandomEnum<NpcDecitionType>();
            behaviorAgent.SetVariableValue("decision", randDecision);
            Decision?.Invoke(randDecision);
            //characterStats.HealthChanged -= OnArmorDamage;
            characterStats.ArmorChanged -= OnArmorDamage;
        }

        public void EnableDamage()
        {
            canDamagePlayer = true;
        }
    }
}