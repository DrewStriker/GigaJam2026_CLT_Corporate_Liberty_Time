
namespace Game.Characters
{
    public interface IState
    {
        public void OnStateEnter();
        public void Update();
        public void FixedUpdate();
        public void OnStateExit();
    }
}

