namespace _Project.CodeBase.Architecture.StateMachine
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}