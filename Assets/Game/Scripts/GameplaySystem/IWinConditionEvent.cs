using System;

namespace Game.GameplaySystem
{
    public interface IWinConditionEvent 
    {
        public event Action <bool> WinConditionMet;
        public void InvokeWinCondition(bool conditionMet);
    }
}
