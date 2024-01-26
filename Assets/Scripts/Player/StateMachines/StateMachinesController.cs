using System.Linq;
using Abstract.EventBus;
using Abstract.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.StateMachines
{
    public class StateMachinesController:MonoBehaviour
    {
        private PlayerStateMachine[] _stateMachines;

        private void Start()
        {
            _stateMachines = GetComponents<PlayerStateMachine>();
        }

        public void InputToStateMachine(InputAction inputAction)
        {
            for (int i = 0; i < _stateMachines.Length; i++)
            {
                _stateMachines[i].InputHandleableState.InputHandle(ref inputAction);                
            }
        }
        
        public void SetFirstOrderOfStateMachine(PlayerStateMachine stateMachine)
        {
            int index = _stateMachines.ToList().IndexOf(stateMachine);
            var stateMachines = _stateMachines.Where(sm => sm != stateMachine).ToArray();
            _stateMachines[0] = _stateMachines[index];
            for (int i = 0; i < stateMachines.Length; i++)
            {
                _stateMachines[i + 1] = stateMachines[i];
            }

            for (int i = 0; i < _stateMachines.Length; i++)
            {
                Debug.Log(_stateMachines[i].GetType());
            }
        }
    }
}