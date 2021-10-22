using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    
    [SerializeField] private Transform player;
    Vector3 offset;
    private float smoothSpeed = 10f;
  
    void Start()
    {
        offset = transform.position;
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
    public void GetToStart()
    {
        transform.position = offset;
    }
    
    
}
