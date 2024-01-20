using Abstract.StateMachine.State;
using PlayerControl.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Abstract.StateMachines.State
{
    public abstract class PlayerState:ScriptableObject,IState,IUpdatable,IInputHandleable
    {
        protected PlayerStateMachine StateMachine;
        public abstract void Enter();

        public abstract void Exit();
        
        public abstract void Update();
        
        public abstract void FixedUpdate();
        public abstract void InputHandle(ref InputAction inputAction);

        public virtual void InitStateMachine(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}