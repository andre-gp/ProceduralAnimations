using UnityEngine;

namespace ProceduralAnimations.Snake
{
    public class StyleSelector : MonoBehaviour
    {
        static int LAST_COLOR = 0;

        [SerializeField] Style[] styleOptions;

        Snake snake;

        private void Awake()
        {
            snake = GetComponent<Snake>();

            snake.UpdateStyle(styleOptions[LAST_COLOR]);
        }

        private void Update()
        {
            for (int i = 0; i < styleOptions.Length; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    snake.UpdateStyle(styleOptions[i]);

                    LAST_COLOR = i;
                }
            }
        }
    }
}
