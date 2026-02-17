using System;

namespace Game.TimeSystem
{
    public interface ITimerManager
    {
        public Timer Timer { get; }
        public float NormalizedTime => Timer.RelativeNormalizedProgress;
        public float AbsNormalizedTime => Timer.AbsNormalizedProgress;
        public int ExtraTime { get; }

        public event Action MainTimeExpired;
        public event Action ExtraTimeExpired;
    }
}

