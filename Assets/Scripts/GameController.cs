using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]SwerveMovement movement;
    [SerializeField] UIController uiController;
    [SerializeField] Level levelController;
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
        levelController.LoadLevel(isReplay);
        
    }
}
