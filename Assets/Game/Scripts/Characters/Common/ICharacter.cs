
using Game.StatsSystem;
using log4net.Util;

namespace Game.Characters
{
    public interface ICharacter
    {
        public AnimationController  AnimationController {get;}
        public CharacterStats characterStats {get;}

    }
}

