using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class MoveObject : MonoBehaviour
{
    LeanDragTranslate movement;
    private float moveSpeed = 10f;
    private void Start()
    {
        movement = GetComponent<LeanDragTranslate>();
        movement.enabled = false;
    }

    private void OnMouseDrag()
    {
        movement.enabled = true;
    }
    private void OnMouseUp()
    {
        movement.enabled = false;
    }
    private void Update()
    {
        if (movement.enabled)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.forward*Time.deltaTime*moveSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.left * Time.deltaTime * moveSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.back * Time.deltaTime * moveSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right * Time.deltaTime * moveSpeed;
            }
        }
    }
}
