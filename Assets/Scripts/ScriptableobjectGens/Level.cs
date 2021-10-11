using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    public int level;
    public Material groundMat;
    public Material pitMat;
    public Color groundColor;
    public Color pitColor;
    public Road[] roads;
    public int[] objectPoolNeededAmount;
}






    

