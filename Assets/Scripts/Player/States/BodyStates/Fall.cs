﻿using System;
using Abstract.StateMachines.State;
using Effects;
using Parameters;
using Player.Signals;
using Player.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States.BodyStates
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player/BodyStates/FallState",fileName = "FallState")]
    public class Fall:PlayerState
    {
        private Rigidbody2D _rb;
        private Transform _tr;
        [SerializeField] private MoveParams _moveParams;
        [SerializeField] private FallParams _fallParams;
        private MoveHorizontalFX _moveFX;
        private FlipFX _flipFX;
        private float _moveX;
        private FallSignal _fallSignal = new();
        public override void Enter()
        {
            _rb.gravityScale *= _fallParams.FallGravityScale;
            _fallSignal.MaxFallVelocity = _fallParams.MaxFallVelocity;
        }

        public override void Exit()
        {
            _rb.gravityScale /= _fallParams.FallGravityScale;
        }
        public override void FixedUpdate()
        {
            _moveFX.Move(_rb,_moveParams,_moveX);
            if (_moveX!=0)
                _flipFX.Flip(_tr,_moveX);
            if (Mathf.Approximately(_rb.velocity.y, 0))
                StateMachine.ChangeState<Idle>(this);
            LimitFallVelocity();
            _fallSignal.FallVelocity = _rb.velocity.y;
            PlayerEventBus.Invoke(_fallSignal);
        }

        private void LimitFallVelocity()
        {
            float fallVelocity = Mathf.Max(_rb.velocity.y, -_fallParams.MaxFallVelocity);
            _rb.velocity = new Vector2(_rb.velocity.x, fallVelocity);
        }
        public override void InputHandle(ref InputAction inputAction)
        {
            if (inputAction == null) return;
            if (inputAction.name == "MoveX")
            {
                _moveX = inputAction.ReadValue<Vector2>().x;
            }
            else
            {
                _moveX = 0;
            }
        }
        private void OnLedgeDetection()
        {
            Debug.Log("!");
        }
        public void InitState(PlayerStateMachine stateMachine, MoveHorizontalFX moveHorizontalFX, FlipFX flipFX, ref Action ledgeDetection)
        {
            StateMachine = stateMachine;
            _moveFX = moveHorizontalFX;
            _flipFX = flipFX;
            _rb = stateMachine.GetComponent<Rigidbody2D>();
            _tr = stateMachine.GetComponent<Transform>();
            ledgeDetection += OnLedgeDetection;
        }
    }
}