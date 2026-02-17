using Game.Characters;
using Game.core;
using UnityEngine;

namespace Game.AgentSystem.Behaviour
{
    [CreateAssetMenu(fileName = "Follow Behaviour", menuName = "Enemy/Follow Behaviour")]
    public class FollowBehaviour : BaseBehaviour
    {
        [SerializeField] private float minDistance = 1;

        public override void Execute(IEnemyCharacter context)
        {
            var canExecute = context.Transform.DistanceTo(playerTransform) > minDistance;
            if (canExecute)
            {
                context.NavMeshAgent.SetDestination(playerTransform.position);
                context.NavMeshAgent.isStopped = false;
                return;
            }

            context.NavMeshAgent.isStopped = true;
        }
    }
}