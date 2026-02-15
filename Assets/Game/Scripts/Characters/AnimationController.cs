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
        public static readonly int Attack2 = Animator.StringToHash("Attack2");
        public static readonly int isJumping = Animator.StringToHash("isJumping");
        public static readonly int velocityH = Animator.StringToHash("velocityH");
        public static readonly int Hurt = Animator.StringToHash("Hurt");
        public static readonly int Death = Animator.StringToHash("Death");
        //...
    }
    
    public readonly struct Animation
    {
        public static readonly Animation Idle  = new(AnimHash.Idle);
        public static readonly Animation Walk  = new(AnimHash.Walk);
        public static readonly Animation Run   = new(AnimHash.Run);
        public static readonly Animation Jump  = new(AnimHash.Jump);
        public static readonly Animation Attack1 = new(AnimHash.Attack1);
        public static readonly Animation Attack2 = new(AnimHash.Attack2);
        public static readonly Animation Hurt = new(AnimHash.Hurt);
        public static readonly Animation Death = new(AnimHash.Death);

        public readonly int Hash { get;  }
        private Animation(int hash)
        {
            Hash = hash;
        }

    }
    
    public readonly struct AnimationParameter
    {
        public static readonly AnimationParameter VelocityH  = new(AnimHash.velocityH);
        public static readonly AnimationParameter IsJumping  = new(AnimHash.isJumping);


        public readonly int Hash { get;  }
        private AnimationParameter(int hash)
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
        public void Play(Animation animation, float fadeTime = 0.1f, int layer = 0)
        {
            Animator.CrossFade(animation.Hash, fadeTime, layer);
        }

        public void SetFloat(AnimationParameter parameter, float value)
        {
            Animator.SetFloat(parameter.Hash, value);
        }
         
        public void SetBool(AnimationParameter parameter, bool value)
        {
            Animator.SetBool(parameter.Hash, value);
        }
         
         public void SetTrigger(AnimationParameter parameter)
         {
             Animator.SetTrigger(parameter.Hash);
         }
         
    }
}