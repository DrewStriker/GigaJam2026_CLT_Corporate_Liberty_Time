using System;

namespace Game.GameplaySystem
{
    public class WinConditionEvent : IWinConditionEvent
    {
        public event Action<bool> WinConditionMet;

        public void InvokeWinCondition(bool conditionMet)
        {
            WinConditionMet?.Invoke(conditionMet);
        }
    }
}
