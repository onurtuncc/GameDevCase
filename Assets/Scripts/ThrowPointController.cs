using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPointController : MonoBehaviour
{
    [SerializeField] ObjectPit objectPit;

    public void ActivateObjectPit()
    {
        objectPit.StartCoroutine(objectPit.CheckPitStatus());
    }
}
