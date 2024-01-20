using Abstract.StateMachines.State;
using Effects;
using Parameters;
using PlayerControl.Signals;
using PlayerControl.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControl.States.BodyStates
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player/BodyStates/MoveState", fileName = "MoveState")]
    public class Move:PlayerState
    {
        [SerializeField] private MoveParams _moveParams;
        private MoveHorizontalFX _moveFX = new();
        private FlipFX _flipFX = new();
        private float _horizontalMovement;
        private Rigidbody2D _rb;
        private Transform _tr;
        public override void Enter()
        {
            PlayerEventBus.Invoke(new MoveSignal(true));
            _horizontalMovement = _tr.localScale.x;
        }
        
        public override void Exit()
        {
            PlayerEventBus.Invoke(new MoveSignal(false));
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            _moveFX.Move(_rb,_moveParams,_horizontalMovement);
            _flipFX.Flip(_tr,_horizontalMovement);
        }

        public override void InputHandle(ref InputAction inputAction)
        {
            if (inputAction.name == "MoveX")
            { 
                _horizontalMovement = inputAction.ReadValue<Vector2>().x;
                if (Mathf.Approximately(_horizontalMovement,0))
                    StateMachine.ChangeState<Idle>(this);
            }

            if (inputAction.name == "Jump")
            {
                StateMachine.ChangeState<Jump>(this);
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