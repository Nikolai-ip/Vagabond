using Abstract.EventBus;

namespace PlayerControl.Signals
{
    public class MoveSignal:ISignal
    {
        public bool IsMoving { get; private set; }

        public MoveSignal(bool isMoving)
        {
            IsMoving = isMoving;
        }
    }
}