namespace _Project.CodeBase.Architecture.StateMachine
{
    public interface IState
    {
        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnExit();
    }
}