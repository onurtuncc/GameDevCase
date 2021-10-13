using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    private float rotationDirection;
    private float rotationZAmount = 60f;
    private float openTime = 1.5f;
    public void OpenDoor()
    {
        if (transform.position.x < 0)
        {
            rotationDirection = 1;
        }
        else
        {
            rotationDirection = -1;
        }
        Vector3 openRotation= new Vector3(0, 0, rotationDirection * rotationZAmount);
        //transform.rotation = Quaternion.Euler(openRotation);
        transform.DORotate(openRotation,openTime);
        Vector3 openPosition = new Vector3(transform.position.x + (1 * -rotationDirection), transform.position.y + 1, transform.position.z);
        //transform.position = openPosition;
        transform.DOMove(openPosition, openTime);
    }
}
