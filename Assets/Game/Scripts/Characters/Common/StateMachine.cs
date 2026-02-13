
namespace Game.Characters
{
    public class StateMachine
    {
        public IState CurrentState { get; protected set; }

        public virtual void Update()
        {
            CurrentState.Update();
        }

        public virtual void FixedUpdate()
        {
            CurrentState.FixedUpdate();
        }
    }
}


