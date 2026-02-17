using Game.Characters;
using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Animation = Game.Characters.Animation;

[Serializable]
[GeneratePropertyBag]
[NodeDescription("PlayAnimation", story: "[Enemy] play [animation]", category: "Action/Animation",
    id: "e096dba6c4b89c852bd620bffe7502f1")]
public partial class PlayAnimationAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyController> Enemy;
    [SerializeReference] public BlackboardVariable<AnimationType> Animation;


    private AnimationController animationController => Enemy.Value.AnimationController;

    protected override Status OnStart()
    {
        var animation = Animations[Animation.Value];
        animationController.Play(animation);
        return Status.Success;
    }

    public static Dictionary<AnimationType, Animation> Animations => new()
    {
        { AnimationType.Idle, Game.Characters.Animation.Idle },
        { AnimationType.Run, Game.Characters.Animation.Run },
        { AnimationType.Attack1, Game.Characters.Animation.Attack1 },
        { AnimationType.Attack2, Game.Characters.Animation.Attack2 },
        { AnimationType.Walk, Game.Characters.Animation.Walk },
        { AnimationType.Talk1, Game.Characters.Animation.Talk1 },
        { AnimationType.Talk2, Game.Characters.Animation.Talk2 }
    };
}