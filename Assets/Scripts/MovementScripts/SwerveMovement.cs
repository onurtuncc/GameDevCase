using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    private SwerveInputSystem _swerveInputSystem;
    Rigidbody rb;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;
    private float xSpeed = 50f;
    private float forwardSpeed = 10f;
    public bool canMove=false;
    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
            swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
            rb.velocity = new Vector3(swerveAmount * xSpeed, 0, forwardSpeed);
           
        }
        
        else rb.velocity = Vector3.zero;
      
    }
}