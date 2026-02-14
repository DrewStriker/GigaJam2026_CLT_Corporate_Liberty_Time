using UnityEngine;

namespace Game.Characters
{
    public static class AnimHash
    {
        public static readonly int Idle = Animator.StringToHash("Idle");
        public static readonly int Walk = Animator.StringToHash("Walk");    
        public static readonly int Run = Animator.StringToHash("Run");
        public static readonly int Jump = Animator.StringToHash("Jump");
        public static readonly int Attack1 = Animator.StringToHash("Attack1");
        //...
    }
    
    public readonly struct Animation
    {
        public static readonly Animation Idle  = new(AnimHash.Idle);
        public static readonly Animation Walk  = new(AnimHash.Walk);
        public static readonly Animation Run   = new(AnimHash.Run);
        public static readonly Animation Jump  = new(AnimHash.Jump);
        public static readonly Animation Attack1 = new(AnimHash.Attack1);

        public readonly int Hash { get;  }
        private Animation(int hash)
        {
            Hash = hash;
        }

    }
    
    public class AnimationController
    {
        public Animator Animator { get; private set; }
        
         public AnimationController(Animator animator)
        {
            Animator = animator;
   
        }

         //Como usar: AnimationController.Play(Animation.Idle). 
         //CHAMA, Papai!
        public void Play(Animation animation, float normalizedTime = 0, int layer = 0)
        {
            Animator.CrossFade(animation.Hash, normalizedTime, layer);
        }
         
         
    }
}