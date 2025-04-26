using ProceduralAnimations.Snake;
using UnityEngine;

namespace ProceduralAnimations
{
    public class RigidMovement : MovementType
    {
        public override void Move(Body head, Vector3 targetPosition, float epsilon)
        {
            // Exponential Smoothing.
            float t = 1.0f - Mathf.Pow(epsilon, Time.deltaTime);
            head.transform.position = Vector3.Lerp(head.transform.position, targetPosition, t);

            var current = head;

            while (current.Previous != null)
            {
                Vector3 direction = current.Previous.transform.position - current.transform.position;

                direction.Normalize();

                direction *= current.Distance;

                current.Previous.transform.position = current.transform.position + direction;

                current = current.Previous;
            }
        }
    }
}
