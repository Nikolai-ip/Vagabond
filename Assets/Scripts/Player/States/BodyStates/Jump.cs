using System;
using Abstract.StateMachines.State;
using Effects;
using Parameters;
using Player.Signals;
using Player.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States.BodyStates
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player/BodyStates/JumpState",fileName = "JumpState")]
    public class Jump:PlayerState
    {
        [SerializeField] private JumpParams _jumpParams;
        [SerializeField] private MoveParams _moveParams;
        private Rigidbody2D _rb;
        private Transform _tr;
        private MoveHorizontalFX _moveFX;
        private FlipFX _flipFX;
        private JumpFX _jumpFX;
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
            if (inputAction == null) return;
            if (inputAction.name == "MoveX")
            { 
                _horizontalMovement = inputAction.ReadValue<Vector2>().x;
            }
            else
            {
                _horizontalMovement = 0;
            }
        }

        public void InitState(PlayerStateMachine stateMachine, MoveHorizontalFX moveHorizontalFX, FlipFX flipFX, JumpFX jumpFX,ref Action ledgeDetection)
        {
            StateMachine = stateMachine;
            _moveFX = moveHorizontalFX;
            _flipFX = flipFX;
            _jumpFX = jumpFX;
            _rb = stateMachine.GetComponent<Rigidbody2D>();
            _tr = stateMachine.GetComponent<Transform>();
        }
    }
}