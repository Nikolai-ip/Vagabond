using Abstract.EventBus;

namespace Player.Signals
{
    public class JumpSignal:ISignal
    {
        public float JumpVelocity { get; set; }
        public float MaxJumpVelocity { get; set; }
        public bool JumpStarted { get; set; }

    }
}