using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ObjectPit : MonoBehaviour
{
    [SerializeField]private Material groundMat;
    [SerializeField] private int neededAmount = 10;
    public static event Action OnLevelFailed;

    private TMP_Text neededAmountText;
    private string textDisplay = "{0}/{1}";
    
    private SwerveMovement playerMovement;
    private Renderer pitRenderer;

    private int amountInPit = 0;
    private float pitDepth=1;
    private float lerpSpeed = 5f;
    private bool isPit = true;
    private float waitTime = 3f;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
        playerMovement = FindObjectOfType<SwerveMovement>();
        pitRenderer = GetComponent<Renderer>();
        neededAmountText = GetComponentInChildren<TMP_Text>();
        neededAmountText.text = string.Format(textDisplay,amountInPit, neededAmount);
    }
    private void PassThePit()
    {
        Destroy(neededAmountText);
        pitRenderer.material.color = groundMat.color;
        transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * pitDepth, lerpSpeed);
        playerMovement.canMove = true;
        isPit = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isPit) return;
        if (collision.gameObject.tag == "Collectable")
        {
            collision.gameObject.SetActive(false);
            amountInPit++;
            neededAmountText.text = string.Format(textDisplay, amountInPit, neededAmount);
            if(amountInPit==1)StartCoroutine(CheckPitStatus());
            
        }
        
    }
    
    IEnumerator CheckPitStatus()
    {
        yield return new WaitForSeconds(waitTime);
        if (amountInPit >= neededAmount)
        {
            PassThePit();
        }
        else
        {
            Debug.Log("Level failed");
            OnLevelFailed.Invoke();
        }

    }
    
}
