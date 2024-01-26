using Abstract.StateMachines.State;
using Effects;
using Parameters;
using Player.Signals;
using Player.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States.BodyStates
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player/BodyStates/MoveState", fileName = "MoveState")]
    public class Move:PlayerState
    {
        [SerializeField] private MoveParams _moveParams;
        private MoveHorizontalFX _moveFX;
        private FlipFX _flipFX;
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
        public override void FixedUpdate()
        {
            _moveFX.Move(_rb,_moveParams,_horizontalMovement);
            _flipFX.Flip(_tr,_horizontalMovement);
        }

        public override void InputHandle(ref InputAction inputAction)
        {
            if (inputAction == null) return;
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
    
        public void InitState(PlayerStateMachine stateMachine, MoveHorizontalFX moveHorizontalFX, FlipFX flipFX)
        {
            StateMachine = stateMachine;
            _moveFX = moveHorizontalFX;
            _flipFX = flipFX;
            _rb = stateMachine.GetComponent<Rigidbody2D>();
            _tr = stateMachine.GetComponent<Transform>();
        }
    }
}