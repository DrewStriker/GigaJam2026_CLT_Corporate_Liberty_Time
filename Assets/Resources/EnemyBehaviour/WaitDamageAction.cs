using Game.Characters;
using System;
using Game.StatsSystem;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable]
[GeneratePropertyBag]
[NodeDescription("WaitDamage", story: "[character] wait damage", category: "Action/Conditional",
    id: "626e8d9c04dbbe5306113efb474167b0")]
public partial class WaitDamageAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyController> Character;

    private CharacterStats CharacterStats => Character.Value.characterStats;

    private Status result = Status.Running;

    protected override Status OnStart()
    {
        CharacterStats.HealthChanged += OnHealthChanged;
        return Status.Running;
    }


    protected override Status OnUpdate()
    {
        return result;
    }

    protected override void OnEnd()
    {
        CharacterStats.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int value)
    {
        result = Status.Success;
    }
}