using UnityEngine;

namespace Effects
{
    public class JumpFX
    {
        public void Jump(Rigidbody2D jumpingBody, float jumpForce)
        {
            jumpingBody.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
        }
    }
}