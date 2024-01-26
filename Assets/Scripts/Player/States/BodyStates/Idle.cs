using System;
using Abstract.StateMachines.State;
using Player.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States.BodyStates
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player/BodyStates/IdleState",fileName = "IdleState")]
    public class Idle:PlayerState
    {
        public override void Enter()
        {
        }
        public override void Exit()
        {
        }
        public override void InputHandle(ref InputAction inputAction)
        {
            if (inputAction == null) return;
            if (inputAction.name == "MoveX")
            {
                float moveX = inputAction.ReadValue<Vector2>().x;
                if (!Mathf.Approximately(moveX,0))
                    StateMachine.ChangeState<Move>(this);
            }
            if (inputAction.name == "Jump")
            {
                StateMachine.ChangeState<Jump>(this);
            }
        }

        public void InitState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}