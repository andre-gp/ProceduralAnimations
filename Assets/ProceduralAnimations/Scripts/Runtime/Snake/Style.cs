using UnityEngine;

namespace ProceduralAnimations.Snake
{
    [CreateAssetMenu(fileName = "Style", menuName = "Snake/Style")]
    public class Style : ScriptableObject
    {
        [SerializeField] AnimationCurve sizeCurve = null;

        [SerializeField] float size = 0.1f;

        [SerializeField] float distance = 0.2f;
        public float Distance => distance;

        [SerializeField] Gradient gradient = null;

        [SerializeField] Color backgroundColor = new Color(0.254f, 0.541f, 0.69f);
        public Color BackgroundColor => backgroundColor;

        public void Apply(Body body, float percentage)
        {
            body.Color = gradient.Evaluate(percentage);
            body.Distance = distance;
            body.Size = sizeCurve.Evaluate(percentage);
        }
    }
}
