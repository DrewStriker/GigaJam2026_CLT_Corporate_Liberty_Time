using System;
using Game.Characters;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Animation = Game.Characters.Animation;

[Serializable]
[GeneratePropertyBag]
[NodeDescription("PlayAnimation", story: "[EnemyController] play [animationType]", category: "Action/Animation",
    id: "376eae5578b6da164783ce44e6e1d7ab")]
public partial class PlayAnimationAction : Action
{
    [SerializeReference] public BlackboardVariable<AnimationType> AnimationType;

    [SerializeReference] public BlackboardVariable<EnemyController> EnemyController;
    [SerializeReference] public BlackboardVariable<bool> holdGraph;
    private AnimationController animationController => EnemyController.Value.AnimationController;

    protected override Status OnStart()
    {
        var hash = Animator.StringToHash("Run");
        animationController.Play(Animation.Run);

        return Status.Success;
    }
}