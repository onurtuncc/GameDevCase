using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]SwerveMovement movement;
    [SerializeField] UIController uiController;

    public void StartGameButton()
    {
        StartGame();
    }
    private void StartGame()
    {
        movement.canMove = true;
        uiController.StartGameplayUI();
    }
}
