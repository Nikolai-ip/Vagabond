using System;
using System.Collections.Generic;
using Abstract.StateMachine.State;
using Abstract.StateMachines;
using Abstract.StateMachines.State;
using UnityEngine;

namespace PlayerControl.StateMachines
{
    public class PlayerStateMachine:MonoBehaviour,IInputStateMachine
    {
        enum  StateMachineType
        {
            Body,
            Hand
        }
        [SerializeField] private StateMachineType _stateMachineType;
        public IInputHandleable InputHandleableState
        {
            get => _currentState;
        }

        [SerializeField] private PlayerState _currentState;
        [field:SerializeField] private List<PlayerState> _states;


        private void Start()
        {
            InitStates();
        }

        private void InitStates()
        {
            foreach (var state in _states)
            {
                state.InitStateMachine(this);
            }
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
        }
    }
}