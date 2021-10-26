using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    private SwerveInputSystem _swerveInputSystem;
    public Rigidbody rb;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;
    public float xSpeed = 50f;
    public float forwardSpeed = 500f;
    public float lerpSpeed = 20f;
    public bool canMove=false;
    private Vector3 pos=Vector3.zero;
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
            Vector3 newVelocity = new Vector3(swerveAmount * xSpeed, 0, forwardSpeed * Time.deltaTime);
            rb.velocity = Vector3.Lerp(rb.velocity, newVelocity, lerpSpeed*Time.deltaTime);

            /*
            pos.x = Time.deltaTime * _swerveInputSystem.MoveFactorX*xSpeed;
            pos.z = Time.deltaTime * forwardSpeed;
            rb.MovePosition(transform.position+ pos);
            Debug.Log("move");
            */


        }

        else
        {
            
            //rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }
      
    }
}