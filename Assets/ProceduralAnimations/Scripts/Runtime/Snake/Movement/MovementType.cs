using UnityEngine;
using ProceduralAnimations.Snake;

namespace ProceduralAnimations
{
    public abstract class MovementType
    {
        public abstract void Move(Body head, Vector3 targetPosition, float epsilon);
    }


}
