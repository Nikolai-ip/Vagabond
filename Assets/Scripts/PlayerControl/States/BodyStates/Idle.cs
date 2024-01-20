﻿using Abstract.StateMachines.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControl.States.BodyStates
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

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {

        }

        public override void InputHandle(ref InputAction inputAction)
        {
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
    }
}