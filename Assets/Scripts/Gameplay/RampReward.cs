using UnityEngine;
using TMPro;

public class RampReward : MonoBehaviour
{
    [SerializeField] Color rewardColor;
    [SerializeField] int rewardAmount;
    private TMP_Text rewardText;
    

    private void Start()
    {
        rewardText = GetComponentInChildren<TMP_Text>();
        rewardText.text = rewardAmount.ToString();
        GetComponent<Renderer>().material.color = rewardColor;
    }
}
