using UnityEngine;

namespace Parameters
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Parameters/MoveParameters", fileName = "MoveParameters")]
    public class MoveParams:ScriptableObject
    {
        [field:SerializeField] public float Speed { get; private set; }
        [field:SerializeField] public float Acceleration { get; private set; }
        [field:SerializeField] public float Deceleration { get; private set; }
        [field:SerializeField] public float VelocityPower { get; private set; }

    }
}