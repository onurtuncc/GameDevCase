using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    private List<Rigidbody> collectedRb = new List<Rigidbody>();
    private float throwPower = 200f;
    private SwerveMovement playerMovement;

    private void Start()
    {
        playerMovement = transform.root.GetComponent<SwerveMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectable")
        {
            collectedRb.Add(other.GetComponent<Rigidbody>());
            Debug.Log("Added to list");
        }
        else if (other.tag == "ThrowPoint")
        {
            ThrowThemAll();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Collectable")
        {
            collectedRb.Remove(other.GetComponent<Rigidbody>());
            Debug.Log("Removed from list");
        }

    }

    private void ThrowThemAll()
    {
        playerMovement.canMove = false;
        
        Debug.Log("Throw");
        foreach(Rigidbody rb in collectedRb)
        {
            rb.AddForce(0, 0, throwPower);
        }
    }
}
