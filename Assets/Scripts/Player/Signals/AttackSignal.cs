using Abstract.EventBus;

namespace Player.Signals
{
    public class AttackSignal:ISignal
    {
        public float AttackSpeed { get; private set; }

        public AttackSignal(float attackSpeed)
        {
            AttackSpeed = attackSpeed;
        }
    }
}