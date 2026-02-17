using Game.Characters;

namespace Game.AgentSystem
{
    public interface IPlayerDetector
    {
        ITarget Player { get; }
    }
}