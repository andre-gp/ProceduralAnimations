using DG.Tweening;
using UnityEngine;

namespace ProceduralAnimations.Snake
{
    public class Snake : MonoBehaviour
    {       
        [SerializeField] Style currentStyle;

        [SerializeField] Body bodyPrefab = null;

        [SerializeField] int bodyLength = 5;

        [SerializeField] float epsilon = 0.3f;

        MovementType movement;

        Body head;

        Camera mainCamera;

        private void Start()
        {
            movement = new SmoothMovement();

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
            
            mainCamera.DOColor(currentStyle.BackgroundColor, 1f);

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

            movement.Move(head, targetPosition, epsilon);
        }
    }
}
