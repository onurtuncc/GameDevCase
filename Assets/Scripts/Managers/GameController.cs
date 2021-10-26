using UnityEngine;


    public class GameController : MonoBehaviour
    {
        [SerializeField] SwerveMovement movement;
        [SerializeField] LevelManager levelController;
        [SerializeField] CameraManager cameraManager;
        [SerializeField] Basket basketController;
        public void StartGameButton()
        {
            StartGameplay();
        }
        public void ReplayButton()
        {
            StartGame(true);
        }
        public void NextLevelButton()
        {
            StartGame(false);
        }

        private void StartGameplay()
        {
            movement.canMove = true;

        }

        private void StartGame(bool isReplay)
        {
            movement.gameObject.transform.position = Vector3.zero;
            movement.rb.velocity = Vector3.zero;
            movement.canMove = false;
            cameraManager.GetToStart();
            basketController.EmptyBasket();
            levelController.PickLevelAndCreate(isReplay);

        }
    }
