using UnityEngine;

namespace Parameters
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Parameters/JumpParameters", fileName = "JumpParameters")]
    public class JumpParams:ScriptableObject
    {
        [field:SerializeField] public float JumpHeight { get; private set; }
    }
}