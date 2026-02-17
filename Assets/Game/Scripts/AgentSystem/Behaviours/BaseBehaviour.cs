using Game.Characters;
using Game.Scripts.AgentSystem.Behaviours;
using UnityEngine;
using UnityEngine.AI;

namespace Game.AgentSystem.Behaviour
{
    public abstract class BaseBehaviour : ScriptableObject, IBehaviour, IPlayerDetector
    {
        public ITarget Player { get; private set; }
        protected Transform playerTransform => Player.Transform;


        public void Initialize(ITarget player)
        {
            Player = player;
        }


        public abstract void Execute(IEnemyCharacter context);
    }
}