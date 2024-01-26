using Abstract.StateMachines.State;
using Player.Signals;
using Player.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States.BodyStates
{
    [CreateAssetMenu(menuName="ScriptableObjects/Player/HandStates/AttackState",fileName = "AttackState")]
    public class Attack:PlayerState
    {
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _damage;
        [SerializeField] private float _impulseForce;
        private bool _canAttack;
        private int _animationsEventCount;
        private Rigidbody2D _rb;
        private Transform _tr;
        public override void Enter()
        {
            PlayerEventBus.Invoke(new AttackSignal(_attackSpeed));
            _canAttack = false;
            _rb.AddForce(Vector2.right * (_tr.localScale.x * _impulseForce),ForceMode2D.Impulse);
        }

        public override void Exit()
        {
            _animationsEventCount = 0;
        }
        public override void InputHandle(ref InputAction inputAction)
        {
            if (inputAction.name == "Attack" && _canAttack)
            {
                StateMachine.ChangeState<Attack>(this);
            }
        }

        public void InitState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
            _rb = stateMachine.GetComponent<Rigidbody2D>();
            _tr = stateMachine.GetComponent<Transform>();
        }

        public override void HandleAnimationEvent()
        {
            _animationsEventCount++;
            switch (_animationsEventCount)
            {
                case 1: MakeDamage();
                    break;
                case 2: _canAttack = true;
                    break;
            }
        }

        private void MakeDamage()
        {
            
        }
        
    }
}