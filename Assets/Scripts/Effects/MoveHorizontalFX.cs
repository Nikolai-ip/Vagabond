using Parameters;
using UnityEngine;

namespace Effects
{
    public class MoveHorizontalFX
    {
        private readonly float _maxMoveForce = 100f;
        public void Move(Rigidbody2D movableBody, MoveParams moveParams, float moveX)
        {
            float targetSpeed = moveX * moveParams.Speed;
            float speedDif = targetSpeed - movableBody.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? moveParams.Acceleration : moveParams.Deceleration;
            float horizontalMovement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, moveParams.VelocityPower) * Mathf.Sign(speedDif);
            //Debug.Log("targetSpeed " +  targetSpeed + " speedDif " + speedDif + " accelRate " + accelRate + " horizontalMovement " + horizontalMovement);
            horizontalMovement = Mathf.Min(horizontalMovement, _maxMoveForce);
            movableBody.AddForce(Vector2.right*horizontalMovement);
        }
    }
}