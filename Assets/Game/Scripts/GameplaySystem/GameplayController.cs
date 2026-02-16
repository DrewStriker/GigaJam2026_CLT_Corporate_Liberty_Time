using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameplaySystem
{
    public class GameplayController : MonoBehaviour
    {
        [Inject] private IGameplayState gameplayState;

        private void Start()
        {
            gameplayState.StateChanged += OnStateChanged;
            gameplayState.SetState(StateType.Intro);
        }

        private void OnDestroy()
        {
            gameplayState.StateChanged -= OnStateChanged;
        }

        private void OnStateChanged(StateType state)
        {
            print(state + "start");
            switch (state)
            {
                case StateType.Intro:
                    OnGameplayIntro();
                    break;
                case StateType.Combat:
                    OnGameplayCombat();
                    break;
                case StateType.End:
                    break;
            }
        }


        private void OnGameplayIntro()
        {
            gameplayState.SetState(StateType.Combat);
        }

        private void OnGameplayCombat()
        {
        }
    }
}