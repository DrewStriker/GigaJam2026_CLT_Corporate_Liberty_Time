using Game.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace Game.AgentSystem
{
    public interface IBehaviour
    {
        void Execute(IEnemyCharacter enemyContext);
    }
}