
using Game.StatsSystem;

namespace Game.Characters
{
    public interface ICharacter
    {
        public AnimationController  AnimationController {get;}
        public CharacterStats characterStats {get;}
    }
}

