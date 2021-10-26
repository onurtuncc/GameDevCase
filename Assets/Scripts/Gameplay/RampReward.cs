using UnityEngine;
using TMPro;

public class RampReward : MonoBehaviour
{
    [SerializeField] Color rewardColor;
    public int rewardAmount;
    private TMP_Text rewardText;
    

    private void Start()
    {
        rewardAmount -= 200;
        rewardText = GetComponentInChildren<TMP_Text>();
        rewardText.text = rewardAmount.ToString();
        GetComponent<Renderer>().material.color = rewardColor;
    }
}
