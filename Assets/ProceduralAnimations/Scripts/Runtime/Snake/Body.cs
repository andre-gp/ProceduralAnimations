using DG.Tweening;
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
                transform.GetChild(0).DOScale(Vector3.one * value, 3f);                
            }
        }

        public Color Color 
        { 
            set
            {
                GetComponentInChildren<Renderer>().material.DOColor(value, 1f);
            } 
        }
    }
}
