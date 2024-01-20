using Abstract.EventBus;

namespace PlayerControl.Signals
{
    public class JumpSignal:ISignal
    {
        public float JumpVelocity { get; set; }
        public float MaxJumpVelocity { get; set; }
        public bool JumpStarted { get; set; }

    }
}