using Abstract.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControl.StateMachines
{
    public class StateMachinesController:MonoBehaviour
    {
        private IInputStateMachine[] _stateMachines;

        private void Start()
        {
            _stateMachines = GetComponents<IInputStateMachine>();
        }

        public void InputToStateMachine(InputAction inputAction)
        {
            for (int i = 0; i < _stateMachines.Length; i++)
            {
                _stateMachines[i].InputHandleableState.InputHandle(ref inputAction);                
            }
        }
    }
}