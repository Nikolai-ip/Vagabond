using UnityEngine.InputSystem;

namespace Abstract.StateMachine.State
{
    public interface IInputHandleable
    {
        void InputHandle(ref InputAction inputAction);
    }
}