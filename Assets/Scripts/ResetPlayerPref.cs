using UnityEngine;

public class ResetPlayerPref : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetInt("currentLevel", 1);
        PlayerPrefs.SetInt("lastLevelIndex", 0);
        PlayerPrefs.SetInt("nextLevelIndex", 1);
    }

    
}
