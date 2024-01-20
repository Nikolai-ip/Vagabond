using System;
using Abstract.StateMachines.State;
using Effects;
using Parameters;
using PlayerControl.Signals;
using PlayerControl.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControl.States.BodyStates
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player/BodyStates/JumpState",fileName = "JumpState")]
    public class Jump:PlayerState
    {
        [SerializeField] private JumpParams _jumpParams;
        [SerializeField] private MoveParams _moveParams;
        private Rigidbody2D _rb;
        private Transform _tr;
        private MoveHorizontalFX _moveFX = new();
        private FlipFX _flipFX = new();
        private JumpFX _jumpFX = new();
        private float _horizontalMovement;
        private float _flipMoveX;
        private float _maxJumpVelocity;
        private JumpSignal _jumpSignal = new();
        public override void Enter()
        {
            float jumpForce = (float)Math.Sqrt(2 * Math.Abs(Physics2D.gravity.y) * _jumpParams.JumpHeight);
            _jumpFX.Jump(_rb,jumpForce);
            _jumpSignal.MaxJumpVelocity = jumpForce / _rb.mass;
            _jumpSignal.JumpStarted = true;
            PlayerEventBus.Invoke(_jumpSignal);
            _jumpSignal.JumpStarted = false;
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            _moveFX.Move(_rb,_moveParams,_horizontalMovement);
            if (_horizontalMovement!=0)
                _flipFX.Flip(_tr,_horizontalMovement);
            if (_rb.velocity.y<0)
                StateMachine.ChangeState<Fall>(this);
            if (Mathf.Approximately(_rb.velocity.y, 0))
                StateMachine.ChangeState<Idle>(this);
            _jumpSignal.JumpVelocity = _rb.velocity.y;
            PlayerEventBus.Invoke(_jumpSignal);
        }
        
        public override void InputHandle(ref InputAction inputAction)
        {
            if (inputAction.name == "MoveX")
            { 
                _horizontalMovement = inputAction.ReadValue<Vector2>().x;
            }
            else
            {
                _horizontalMovement = 0;
            }
        }

        public override void InitStateMachine(PlayerStateMachine stateMachine)
        {
            base.InitStateMachine(stateMachine);
            _rb = stateMachine.GetComponent<Rigidbody2D>();
            _tr = stateMachine.GetComponent<Transform>();
        }
    }
}