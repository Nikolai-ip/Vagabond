
using Abstract.StateMachines.State;

namespace Abstract.StateMachines
{
    public interface IStateMachine
    {
        void ChangeState<T>(object sender) where T : IState;
    }
}