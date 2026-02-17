using Game.AgentSystem.Behaviour;
using Game.Characters;
using Game.core;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace Game.Scripts.AgentSystem.Behaviours
{
    [CreateAssetMenu(fileName = "Flee Behaviour", menuName = "Enemy/feel behaviour")]
    public class FleeBehaviour : BaseBehaviour
    {
        public override void Execute(IEnemyCharacter context)
        {
            var playerDirection = context.Transform.DirectionTo(playerTransform);
            var fleeDestination = context.Transform.position - playerDirection;
            context.NavMeshAgent.SetDestination(fleeDestination);
        }
    }
}