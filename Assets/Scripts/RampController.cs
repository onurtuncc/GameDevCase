using UnityEngine;
using DG.Tweening;

public class RampController : MonoBehaviour
{
    [SerializeField]private Transform rampTop;
    [SerializeField] private Transform rampStart;
    [SerializeField] private Transform finishLine;
    private float rampClimbTime = 2f;
    private float toRampTime = 2f;
    private float minThrowPoint = 10f;


    public void RampState(Transform player)
    {
        Sequence rampSequence = DOTween.Sequence();
        rampSequence.Append(player.DOMove(rampStart.position, toRampTime));
        rampSequence.Append(player.DOMove(rampTop.position+Vector3.back, rampClimbTime));
        rampSequence.Join(player.DORotateQuaternion(rampTop.rotation, rampClimbTime));
        Debug.Log(rampTop.position.z + minThrowPoint);
        rampSequence.Append(player.DOJump(new Vector3(0, 0, rampTop.position.z+minThrowPoint), 20, 1, 5));
        rampSequence.Append(player.DOShakeRotation(0.2f, 10, 2));
        rampSequence.Append(player.DORotate(Vector3.zero, 1f));
        rampSequence.Append(player.DOMoveZ(finishLine.position.z-1, 2f));

    }
    
}
