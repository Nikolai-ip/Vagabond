using System;
using System.Collections.Generic;
using Abstract.StateMachine.State;
using Abstract.StateMachines;
using Abstract.StateMachines.State;
using Effects;
using Player.States.BodyStates;
using UnityEngine;

namespace Player.StateMachines
{
    public class PlayerStateMachine:MonoBehaviour,IInputStateMachine
    {
        public IInputHandleable InputHandleableState => _currentState;
        
        [SerializeField] private PlayerState _currentState;
        private List<PlayerState> _states;
        [SerializeField] private Idle _idleState;
        [SerializeField] private Fall _fallState;
        [SerializeField] private Jump _jumpState;
        [SerializeField] private Move _moveState;
        [SerializeField] private Attack _attackState;
        private void Start()
        {
            InitStates();
        }

        private event Action LedgeFound;
        private void InitStates()
        {
            var moveFX = new MoveHorizontalFX();
            var jumpFX = new JumpFX();
            var flipFX = new FlipFX();
            _jumpState.InitState(this, moveFX, flipFX, jumpFX, ref LedgeFound);
            _fallState.InitState(this, moveFX, flipFX, ref LedgeFound);
            _moveState.InitState(this, moveFX, flipFX);
            _attackState.InitState(this);
            _idleState.InitState(this);
            _states = new List<PlayerState>()
            {
                _idleState, _fallState, _jumpState, _moveState, _attackState
            };
        }
        public void ChangeState<T>(object sender) where T : IState
        {
            if (sender is not IState)
                throw new Exception("An Object not of type BodyState is trying to change the current state");
            var newState = _states.Find(state=> state.GetType() == typeof(T));
            if (newState != null)
            {
                _currentState.Exit();
                _currentState = newState;
                _currentState.Enter();
            }
            else
            {
                throw new Exception($"{typeof(T)} doesn't have contains in the states");
            }
        }
        
        private void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }

        private void Update()
        {
            _currentState.Update();
            if (Input.GetKeyDown(KeyCode.Z))
            {
                LedgeFound?.Invoke();
            }
        }
    }
}