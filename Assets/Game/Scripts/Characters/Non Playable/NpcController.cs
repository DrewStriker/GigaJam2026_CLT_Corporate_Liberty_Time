using System;
using Game.Scripts.Core;
using Unity.Behavior;

namespace Game.Characters
{
    [BlackboardEnum]
    public enum NpcDecitionType
    {
        Flee,
        Chase
    }

    public class NpcController : EnemyController
    {
        public event Action<NpcDecitionType> Decision;

        protected override void Start()
        {
            characterStats.HealthChanged += OnDamage;
            base.Start();
            behaviorAgent.SetVariableValue("NpcController", this);
        }

        protected void OnDestroy()
        {
            characterStats.HealthChanged -= OnDamage;
        }

        private void OnDamage(int obj)
        {
            var randDecision = EnumExtensions.GetRandomEnum<NpcDecitionType>();
            behaviorAgent.SetVariableValue("decision", randDecision);
            Decision?.Invoke(randDecision);
            characterStats.HealthChanged -= OnDamage;
        }
    }
}