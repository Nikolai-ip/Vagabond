using UnityEngine;

namespace Effects
{
    public class FlipFX
    {
        public void Flip(Transform tr, float moveX)
        {
            float sign = Mathf.Sign(moveX);
            var localScale = tr.localScale;
            localScale = new Vector3(sign, localScale.y);
            tr.localScale = localScale;
        }
    }
}