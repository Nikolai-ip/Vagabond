using Abstract.EventBus;

namespace Player.Signals
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