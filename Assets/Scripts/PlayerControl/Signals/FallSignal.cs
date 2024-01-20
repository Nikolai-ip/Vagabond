using Abstract.EventBus;

namespace PlayerControl.Signals
{
    public class FallSignal:ISignal
    {
        public float FallVelocity { get; set; }
        public float MaxFallVelocity { get; set; }
    }
}