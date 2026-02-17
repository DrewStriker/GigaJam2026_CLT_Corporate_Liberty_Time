using Game.Characters;
using System;
using Game.StatsSystem;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable]
[GeneratePropertyBag]
[NodeDescription("WaitDamage", story: "[npc] wait damage", category: "Action/Conditional",
    id: "626e8d9c04dbbe5306113efb474167b0")]
public partial class WaitDamageAction : Action
{
    [SerializeReference] public BlackboardVariable<NpcController> npc;

    // private CharacterStats CharacterStats => npc.Value.characterStats;

    private Status result = Status.Running;

    protected override Status OnStart()
    {
        npc.Value.Decision += OnDecided;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return result;
    }

    protected override void OnEnd()
    {
        npc.Value.Decision -= OnDecided;
    }

    private void OnDecided(NpcDecitionType obj)
    {
        result = Status.Success;
    }
}