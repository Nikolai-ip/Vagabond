using Abstract.StateMachine.State;
using Player.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Abstract.StateMachines.State
{
    public abstract class PlayerState:ScriptableObject,IState,IUpdatable,IInputHandleable
    {
        protected PlayerStateMachine StateMachine;
        public abstract void Enter();

        public abstract void Exit();
        
        public virtual void Update(){}
        
        public virtual void FixedUpdate(){}
        public abstract void InputHandle(ref InputAction inputAction);
        
        public virtual void HandleAnimationEvent(){}
        public virtual void OnCollisionWithWall(){}
        
    }
}