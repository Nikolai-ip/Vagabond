using Abstract.EventBus;
using PlayerControl.Signals;
using UnityEngine;

namespace PlayerControl.AnimationModule
{
    public class PlayerAnimator:MonoBehaviour
    {
        private Animator _animator;
        private readonly int _isRunningID = Animator.StringToHash("IsRunning");
        private static readonly int JumpStart = Animator.StringToHash("JumpStart");
        [SerializeField] private float _percentOfInitialVelocityToPlayAnim;
        [SerializeField] private float _percentOfFallVelocityToPlayAnim;
        private static readonly int JumpVelocityCloseToZero = Animator.StringToHash("JumpVelocityCloseToZero");
        private static readonly int JumpFinish = Animator.StringToHash("JumpFinish");
        private static readonly int FallHightVelocity = Animator.StringToHash("FallHightVelocity");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            PlayerEventBus.Subscribe<MoveSignal>(PlayMoveAnimation);
            PlayerEventBus.Subscribe<JumpSignal>(PlayJumpAnimation);
            PlayerEventBus.Subscribe<FallSignal>(PlayFallAnimation);
        }

        private void PlayMoveAnimation(MoveSignal moveSignal)
        {
            _animator.SetBool(_isRunningID, moveSignal.IsMoving);
        }

        private void PlayJumpAnimation(JumpSignal jumpSignal)
        {
            if (jumpSignal.JumpStarted)
            {
                _animator.SetTrigger(JumpStart);
            }
            else if (jumpSignal.JumpVelocity * 100 / jumpSignal.MaxJumpVelocity < _percentOfInitialVelocityToPlayAnim)
            {
                _animator.SetTrigger(JumpVelocityCloseToZero);
            }
        }

        private void PlayFallAnimation(FallSignal fallSignal)
        {
            if (Mathf.Approximately(fallSignal.FallVelocity, 0))
            {
                _animator.SetTrigger(JumpFinish);
                _animator.ResetTrigger(FallHightVelocity);
                _animator.ResetTrigger(JumpVelocityCloseToZero);
            }
            else if (-fallSignal.FallVelocity * 100 / fallSignal.MaxFallVelocity > _percentOfFallVelocityToPlayAnim)
            {
                _animator.SetTrigger(FallHightVelocity);
            }
        }
    }
}