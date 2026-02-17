using Game.Characters;
using System;
using Game.core;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable]
[GeneratePropertyBag]
[NodeDescription("Flee", story: "[enemy] Flee from the [player]", category: "Action",
    id: "0f2c626ea93614d7a5344b6ab52cbc07")]
public partial class FleeAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyController> Enemy;
    [SerializeReference] public BlackboardVariable<PlayerController> Player;

    private Transform playerTransform;
    private Transform enemyTransform;
    private NavMeshAgent navMeshAgent;

    protected override Status OnStart()
    {
        playerTransform = Player.Value.Transform;
        enemyTransform = Enemy.Value.Transform;
        navMeshAgent = Enemy.Value.NavMeshAgent;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var playerDirection = enemyTransform.DirectionTo(playerTransform);
        var fleeDestination = enemyTransform.position - playerDirection;
        navMeshAgent.SetDestination(fleeDestination);

        return Status.Running;
    }
}