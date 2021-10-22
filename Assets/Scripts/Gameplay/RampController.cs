using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RampController : MonoBehaviour
{
    [SerializeField]private Transform rampTop;
    [SerializeField] private Transform rampStart;
    [SerializeField] private Transform finishLine;

    [HideInInspector] public Text popUpGoldText;
    //[HideInInspector]public Image TapBar;
    private float rampClimbTime = 2f;
    private float flightTime = 4f;
    private float toRampTime = 2f;
    private float minThrowPoint = 10f;
    private float maxThrowPoint = 50f;
    private bool isInRampPhase=false;
    [HideInInspector] public float FillAmount=0;
    private float throwPoint = 0;
    private float pointPerClick = 2f;
    Sequence rampSequence;
    Transform playerTransform;

    private void Awake()
    {
        rampSequence = DOTween.Sequence();
    }

    public void RampState(Transform player)
    {
        isInRampPhase = true;
        playerTransform = player;
        rampSequence = DOTween.Sequence();
        rampSequence.Append(player.DOMove(rampStart.position, toRampTime));
        rampSequence.Append(player.DOMove(rampTop.position+Vector3.back, rampClimbTime));
        rampSequence.Join(player.DORotateQuaternion(rampTop.rotation, rampClimbTime));
        //Debug.Log(rampTop.position.z + minThrowPoint);
        /*
        rampSequence.Append(player.DOJump(new Vector3(0, 0, rampTop.position.z+minThrowPoint+throwPoint), 20, 1, 5));
        rampSequence.Append(player.DOShakeRotation(0.2f, 10, 2));
        rampSequence.Append(player.DORotate(Vector3.zero, 1f));
        rampSequence.Append(player.DOMoveZ(finishLine.position.z-1, 2f));
        */

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
            if (!rampSequence.IsActive())
            {
                isInRampPhase = false;
                if (throwPoint > maxThrowPoint) throwPoint = maxThrowPoint;
                Sequence throwSequence = DOTween.Sequence();
                throwSequence.Append(playerTransform.DOJump(new Vector3(0, 0, rampTop.position.z + minThrowPoint + throwPoint), 20, 1, flightTime,false).
                    SetEase(Ease.InOutQuart));
                
                throwSequence.Join(playerTransform.DOShakeRotation(flightTime, 10, 5));
                throwSequence.Join(playerTransform.DORotate(Vector3.zero, flightTime+1));
                throwSequence.Append(playerTransform.DOShakeRotation(0.2f, 10, 2));
                throwSequence.Join(playerTransform.DORotate(Vector3.zero, 1f));
                throwSequence.Append(popUpGoldText.DOText("+"+CalculateGold(),1f));
                throwSequence.Append(playerTransform.DOMoveZ(finishLine.position.z - 1, 2f));
             
                
            }
            
        }
    }
    public int CalculateGold()
    {
        int levelScore = 300 + (int)Mathf.Ceil(throwPoint / 5)*20;
        ScoreManager.ScoreManagerInstance.Score = levelScore;
        ScoreManager.ScoreManagerInstance.AddLevelScore();
        return levelScore;
    }
    

}
