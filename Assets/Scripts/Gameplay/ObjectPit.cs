using System.Collections;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;
using Lean.Pool;

    public class ObjectPit : MonoBehaviour
    {
        [SerializeField] private Door leftDoor;
        [SerializeField] private Door rightDoor;

        [HideInInspector] public Material groundMaterial;
        [HideInInspector] public Color groundColor;
        [HideInInspector] public bool isBonus;
        public int neededAmount = 10;


        private TMP_Text neededAmountText;
        private string textDisplay = "{0}/{1}";
        
        private SwerveMovement playerMovement;
        private Renderer pitRenderer;

        private int amountInPit = 0;
        private bool isPit = true;
        private float waitTime = 3f;
        private float ascendingTime = 0.7f;

        public static event Action<ObjectPit> OnLevelFailed = delegate { };

        // Start is called before the first frame update
        void Start()
        {

            playerMovement = FindObjectOfType<SwerveMovement>();
            pitRenderer = GetComponent<Renderer>();
            neededAmountText = GetComponentInChildren<TMP_Text>();
            if (!isBonus)
            {
                neededAmountText.text = string.Format(textDisplay, amountInPit, neededAmount);
            }
            
            
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

                LeanPool.Despawn(collision.gameObject);

                amountInPit++;
                if (isBonus) neededAmountText.text = amountInPit.ToString();

                else neededAmountText.text = string.Format(textDisplay, amountInPit, neededAmount);

            }

        }


        public IEnumerator CheckPitStatus()
        {

            yield return new WaitForSeconds(waitTime);
            if (isBonus)
            {
                ScoreManager.ScoreManagerInstance.Score = amountInPit;
                ScoreManager.ScoreManagerInstance.AddLevelScore();
                PassThePit();
            }
            else
            {
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

    }

