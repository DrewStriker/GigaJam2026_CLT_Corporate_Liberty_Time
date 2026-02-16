using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameplaySystem
{
    public interface IGameplayState
    {
        public StateType CurrentState { get; }
        public event Action<StateType> StateChanged;
        void SetState(StateType newState);
    }

    public class GameplayStateController : IGameplayState
    {
        public StateType CurrentState { get; private set; }
        public event Action<StateType> StateChanged;

        public void SetState(StateType newState)
        {
            CurrentState = newState;
            StateChanged?.Invoke(CurrentState);
        }
    }
}