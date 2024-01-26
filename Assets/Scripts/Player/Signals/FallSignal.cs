using Abstract.EventBus;

namespace Player.Signals
{
    public class FallSignal:ISignal
    {
        public float FallVelocity { get; set; }
        public float MaxFallVelocity { get; set; }
    }
}