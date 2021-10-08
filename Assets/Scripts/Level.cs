using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject[] levelPrefabs;
    [SerializeField] private GameObject player;
    int currentLevel;
    GameObject currentLevelPrefab;

    private void Start()
    {
        //PlayerPrefs.SetInt("currentLevel", 1);
        
        LoadLevel(false);
    }
    
    public void LoadLevel(bool isReplay)
    {
        //Scene transition
        if (!isReplay)
        {
            currentLevel = PlayerPrefs.GetInt("currentLevel");
            Debug.Log(currentLevel);
            if (currentLevel > levelPrefabs.Length)
            {
                currentLevel = Random.Range(1, levelPrefabs.Length + 1);
            }
            
        }
        player.transform.position = Vector3.zero;
        Destroy(currentLevelPrefab);
        currentLevelPrefab=Instantiate(levelPrefabs[currentLevel - 1]);

    }
    
    
}
