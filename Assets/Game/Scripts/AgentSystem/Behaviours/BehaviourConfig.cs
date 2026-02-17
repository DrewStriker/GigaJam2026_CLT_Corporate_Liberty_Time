using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.AgentSystem.Behaviours
{
    [Serializable]
    public class BehaviourConfig
    {
        public Transform Transform;
        public NavMeshAgent NavMeshAgent;
        public Animator Animator;
    }
}