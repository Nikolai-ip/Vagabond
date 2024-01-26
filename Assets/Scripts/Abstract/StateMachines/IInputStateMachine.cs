using Abstract.StateMachine.State;

namespace Abstract.StateMachines
{
    public interface IInputStateMachine:IStateMachine
    {
         IInputHandleable InputHandleableState { get; }
    }
}