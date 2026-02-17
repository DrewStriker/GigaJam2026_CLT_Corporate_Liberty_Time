using UnityEngine;

namespace Game.AgentSystem
{
    public interface IAgentMovement
    {
        void MoveTo(Vector3 target);
        void Stop();
    }
}