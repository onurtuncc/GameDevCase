using System;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    private List<Rigidbody> collectedRb = new List<Rigidbody>();
    private float throwPower = 150f;
    private SwerveMovement playerMovement;
    public static event Action OnLevelCompleted;
    public static event Action<RampController> OnRampEnter;

    private void Start()
    {
        playerMovement = transform.root.GetComponent<SwerveMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectable")
        {
            collectedRb.Add(other.GetComponent<Rigidbody>());
           
        }
        else if (other.tag == "ThrowPoint")
        {
            other.GetComponent<ThrowPointController>().ActivateObjectPit();
            ThrowThemAll();
            
        }
        else if (other.tag == "Finish")
        {
            PlayerPrefs.SetInt("currentLevel", PlayerPrefs.GetInt("currentLevel") + 1);
            OnLevelCompleted.Invoke();
        }
        else if(other.tag == "RampStart")
        {

            playerMovement.canMove = false;
            var rampController = other.GetComponent<RampController>();
            rampController.RampState(playerMovement.transform);
            OnRampEnter.Invoke(rampController);
            
            
        }
        

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Collectable")
        {
            collectedRb.Remove(other.GetComponent<Rigidbody>());
           
        }

    }

    private void ThrowThemAll()
    {
        playerMovement.canMove = false;

        foreach(Rigidbody rb in collectedRb)
        {
            if(rb!=null) rb.AddForce(0, 0, throwPower);

        }
    }
}
