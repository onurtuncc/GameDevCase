using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    public enum LevelEndType { Normal,Ramp};
    public Material groundMat;
    public Material pitMat;
    public Color groundColor;
    public Color pitColor;
    public Road[] roads;
    public Collectableobject[] collectableobjects;
    public int[] objectPoolNeededAmount;
    public bool isBonusLevel;
    public LevelEndType levelEndType;
    
}






    

