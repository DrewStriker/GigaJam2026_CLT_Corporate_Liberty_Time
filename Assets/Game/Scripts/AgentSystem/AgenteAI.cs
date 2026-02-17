using Game.AgentSystem.Behaviour;
using Game.Characters;
using Game.Scripts.AgentSystem.Behaviours;
using UnityEngine;
using Zenject;

namespace Game.AgentSystem
{
    public class AgenteAI : MonoBehaviour
    {
        [SerializeField] private BaseBehaviour Behaviours;
        [SerializeField] private BehaviourConfig BehaviourConfig;
        [Inject] private ITarget Player;
        private IEnemyCharacter context;

        private void Awake()
        {
            context = GetComponent<IEnemyCharacter>();
            Behaviours.Initialize(Player);
        }

        protected virtual void Update()
        {
            Behaviours.Execute(context);
        }

        private void CheckBehaviorPriority()
        {
        }
    }
}