using UnityEngine;

namespace ProceduralAnimations.Snake
{
    public class Snake : MonoBehaviour
    {       
        [SerializeField] Style currentStyle;

        [SerializeField] Body bodyPrefab = null;

        [SerializeField] int bodyLength = 5;

        [SerializeField] float epsilon = 0.3f;

        Body head;

        Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;

            mainCamera.backgroundColor = currentStyle.BackgroundColor;

            CreateSnake();
        }

        public void CreateSnake()
        {           
            Vector3 startPos = transform.position;

            head = Instantiate(bodyPrefab, startPos, Quaternion.identity, transform);
            currentStyle.Apply(head, 0);

            Body last = head;

            for (int i = 0; i < bodyLength - 1; i++)
            {
                Body current = Instantiate(bodyPrefab, startPos + new Vector3(i * currentStyle.Distance, 0, 0), Quaternion.identity, transform);

                currentStyle.Apply(current, i / (float)bodyLength);

                current.Next = last;
                last.Previous = current;

                last = current;
            }
        }

        public void UpdateStyle(Style newStyle)
        {
            currentStyle = newStyle;

            if (mainCamera == null)
                return; // Has not initialized

            mainCamera.backgroundColor = currentStyle.BackgroundColor;

            Body current = head;
            int position = 0;
            while(current != null)
            {
                currentStyle.Apply(current, position / (float) bodyLength);
                position += 1;
                current = current.Previous;
            }
        }

        private void Update()
        {
            MoveSnake();
        }

        private void MoveSnake()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mainCamera.transform.position.y;
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePos);

            // Exponential Smoothing.
            float t = 1.0f - Mathf.Pow(epsilon, Time.deltaTime);
            head.transform.position = Vector3.Lerp(head.transform.position, targetPosition, t);

            var current = head.Previous;

            while (current != null)
            {
                Vector3 direction = current.transform.position - current.Next.transform.position;

                direction.Normalize();

                direction *= current.Distance;

                current.transform.position = current.Next.transform.position + direction;

                current = current.Previous;
            }
        }
    }
}
