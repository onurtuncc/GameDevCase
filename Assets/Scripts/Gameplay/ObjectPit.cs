using System.Collections;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class ObjectPit : MonoBehaviour
{
    [SerializeField] private Door leftDoor;
    [SerializeField] private Door rightDoor;

    [HideInInspector] public Material groundMaterial;
    [HideInInspector]public Color groundColor;
    public int neededAmount = 10;
    public static event Action<ObjectPit> OnLevelFailed=delegate { };

    private TMP_Text neededAmountText;
    private string textDisplay = "{0}/{1}";
    
    private SwerveMovement playerMovement;
    private Renderer pitRenderer;

    private int amountInPit = 0;
    private bool isPit = true;
    private float waitTime = 3f;
    private float ascendingTime = 1;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
        playerMovement = FindObjectOfType<SwerveMovement>();
        pitRenderer = GetComponent<Renderer>();
        neededAmountText = GetComponentInChildren<TMP_Text>();
        neededAmountText.text = string.Format(textDisplay,amountInPit, neededAmount);
    }
    
    public void PassThePit()
    {
        Destroy(neededAmountText);
        pitRenderer.material = groundMaterial;
        pitRenderer.material.DOColor(groundColor, ascendingTime);
        transform.DOMoveY(0, ascendingTime);
        isPit = false;
        leftDoor.OpenDoor();
        rightDoor.OpenDoor();
        playerMovement.canMove = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isPit) return;
        if (collision.gameObject.tag == "Collectable")
        {
            collision.gameObject.SetActive(false);
            amountInPit++;
            neededAmountText.text = string.Format(textDisplay, amountInPit, neededAmount);
            
        }
        
    }
    
    
    public IEnumerator CheckPitStatus()
    {

        yield return new WaitForSeconds(waitTime);
        if (amountInPit >= neededAmount)
        {
            PassThePit();
        }
        else
        {
            Debug.Log("Level failed");
            OnLevelFailed.Invoke(this);
        }

    }
    
}
