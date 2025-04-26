using UnityEngine;

namespace ProceduralAnimations.Snake
{
    public class Body : MonoBehaviour
    {
        private Body previous = null;

        public Body Previous { get { return previous; } set { previous = value; } }

        private Body next = null;

        public Body Next { get { return next; } set { next = value; } }

        private float distance = 1f;
        public float Distance { get { return distance; } set { distance = value; } }

        public float Size
        {
            set
            {
                transform.GetChild(0).transform.localScale = Vector3.one * value;
            }
        }

        public Color Color 
        { 
            set
            {
                GetComponentInChildren<Renderer>().material.color = value;
            } 
        }
    }
}
