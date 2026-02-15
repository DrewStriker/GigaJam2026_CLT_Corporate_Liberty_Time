
using DamageSystem;
using Game.StatsSystem;
using log4net.Util;
using UnityEngine;

namespace Game.Characters
{
    public interface ICharacter : IDamageable
    {
        public AnimationController  AnimationController {get;}
        public CharacterStats characterStats {get;}
        public Rigidbody Rigidbody { get;  }

    }
}

