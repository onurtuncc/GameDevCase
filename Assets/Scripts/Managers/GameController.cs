using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]SwerveMovement movement;
    [SerializeField] UIController uiController;
    [SerializeField] LevelManager levelController;
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
        uiController.StartGameplayUI();
    }

    private void StartGame(bool isReplay)
    {
        //Transition effect
        movement.canMove = false;
        uiController.LevelBeginUI();
        levelController.PickLevelAndCreate(isReplay);
        
    }
}
