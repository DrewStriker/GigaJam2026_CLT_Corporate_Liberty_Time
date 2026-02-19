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
            characterStats.HealthChanged += OnDamage;
            base.Start();
            behaviorAgent.SetVariableValue("NpcController", this);
        }

        protected override void OnCollisionEnter(Collision other)
        {
            if (canDamagePlayer) base.OnCollisionEnter(other);
        }

        private void OnDamage(int obj)
        {
            var randDecision = EnumExtensions.GetRandomEnum<NpcDecitionType>();
            behaviorAgent.SetVariableValue("decision", randDecision);
            Decision?.Invoke(randDecision);
            characterStats.HealthChanged -= OnDamage;
        }

        public void EnableDamage()
        {
            canDamagePlayer = true;
        }
    }
}