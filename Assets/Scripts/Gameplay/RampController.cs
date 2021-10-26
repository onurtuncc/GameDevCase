using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class RampController : MonoBehaviour
{

    [SerializeField] private Transform finishLine;
    
    public Transform[] path;
    private Vector3[] pathPositions;
    [HideInInspector] public Text popUpGoldText;
 
    private float rampClimbTime = 4f;
   
    private float minThrowPoint = 15f;
    private float maxThrowPoint = 30f;
    private bool isInRampPhase=false;
    [HideInInspector] public float FillAmount=0;
    private float throwPoint = 0;
    private float pointPerClick = 1.5f;
    Transform playerTransform;
    Rigidbody rb;
    RigidbodyConstraints defaultConstraints;

    
    private void GetPositions()
    {
        pathPositions = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            pathPositions[i] = path[i].position;
        }
    }
    

    public void RampState(Transform player)
    {
        Debug.Log("In Ramp State");
        GetPositions();
        isInRampPhase = true;
        playerTransform = player;
        StartCoroutine(JumpRoutine());
        Sequence rampSeq = DOTween.Sequence();
        rampSeq.Append(player.DOPath(pathPositions, rampClimbTime).SetEase(Ease.InOutSine));
        rampSeq.Join(player.DORotate(new Vector3(-30, 0, 0), 1.1f).SetDelay(0.5f));

    }
    public void Update()
    {
        if (isInRampPhase)
        {
            if (Input.GetMouseButtonDown(0))
            {
                throwPoint+=pointPerClick;
                FillAmount = throwPoint / maxThrowPoint;
                
            }
        }
    }
    public void EndLevel(int reward)
    {
        if (rb == null || playerTransform == null) return;
        Debug.Log("Ramp Level END started");
        rb.constraints = defaultConstraints;
        Sequence levelEndSequence = DOTween.Sequence();
        levelEndSequence.Append(popUpGoldText.DOText("+" + reward.ToString(), 1f));
        levelEndSequence.Append(playerTransform.DORotate(new Vector3(playerTransform.rotation.x,0,playerTransform.rotation.z), 2f));
       
        levelEndSequence.Append(playerTransform.DOMove(finishLine.position + Vector3.up * 0.49f+Vector3.back*4, 3f));


    }
    
   
    IEnumerator JumpRoutine()
    {
        yield return new WaitForSeconds(rampClimbTime-1.2f);
        rb = playerTransform.GetComponent<Rigidbody>();
        defaultConstraints = rb.constraints;
        rb.constraints = RigidbodyConstraints.None;
        isInRampPhase = false;
        if (throwPoint > maxThrowPoint) throwPoint = maxThrowPoint;
        rb.AddForce(0, 30+throwPoint/5,(minThrowPoint+throwPoint), ForceMode.Impulse);
        playerTransform.GetComponent<ClaimReward>().checkClaim = true;
    }

    

}
