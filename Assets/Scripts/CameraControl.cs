using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]private Transform target;
    private Vector3 offset;
    private Vector3 newPos;
    [SerializeField] private float lerpSpeed = 3f;

    void Start()
    {
        //getting players transform
        

        offset = transform.position;
    }


    void LateUpdate()
    {
        //Following player along
        if (target != null)
        {
            newPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, newPos, lerpSpeed * Time.deltaTime);
        }


    }
}
