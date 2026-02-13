
namespace Game.Characters
{
    public abstract class BaseState : IState
    {
        public BaseState(StateMachine stateMachine) => this.stateMachine = stateMachine;

        protected StateMachine stateMachine;
        public abstract void FixedUpdate();

        public abstract void OnStateEnter();

        public abstract void OnStateExit();

        public abstract void Update();
    }
}

