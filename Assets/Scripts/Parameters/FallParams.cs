using UnityEngine;

namespace Parameters
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Parameters/FallParams", fileName = "FallParams")]
    public class FallParams:ScriptableObject
    {
        [field:SerializeField] public float MaxFallVelocity { get; private set; }
        [field:SerializeField] public float FallGravityScale { get; private set; }
    }
}