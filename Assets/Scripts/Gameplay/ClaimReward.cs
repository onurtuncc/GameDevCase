using System;
using UnityEngine;

public class ClaimReward : MonoBehaviour
{
    
    [SerializeField]private RampReward lastClaim;
    [SerializeField]private Rigidbody rb;
    public static event Action<int> RewardClaimed = delegate { };
    public bool checkClaim = false;
    private RampController activeRamp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
  
    }
    private void OnEnable()
    {
        Basket.OnRampEnter += ActivateClaim;
    }
    private void OnDestroy()
    {
        Basket.OnRampEnter -= ActivateClaim;
    }
    private void ActivateClaim(RampController rampController)
    {
        activeRamp = rampController;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!checkClaim) return;

        if (other.tag == "Reward" && rb.velocity == Vector3.zero)
        {
            CalculateReward();
            checkClaim = false;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Reward")
        {
            lastClaim = other.GetComponent<RampReward>();
        }
    }

    private void CalculateReward()
    {
        if (lastClaim == null) return;
        activeRamp.EndLevel(lastClaim.rewardAmount);
        ScoreManager.ScoreManagerInstance.Score = lastClaim.rewardAmount;
        ScoreManager.ScoreManagerInstance.AddLevelScore();
    }


}
