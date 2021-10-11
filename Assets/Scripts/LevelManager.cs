using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    public Level[] levels;
    public GameObject downPlatform;
    public GameObject finishPlatform;
    private GameObject plane;
    private float prevRoadLength = 0;
    private float prevPitLength = 0;
    private float defaultPitLength = 10f;
    private GameObject pit;
    private int currentLevel;
    private GameObject currentLevelPrefab;


    private void Start()
    {
        
        PickLevelAndCreate(false);
    }


    public void PickLevelAndCreate(bool isReplay)
    {
        if (!isReplay)
        {

            currentLevel = PlayerPrefs.GetInt("currentLevel");
            Debug.Log(currentLevel);
            if (currentLevel > levels.Length)
            {

                currentLevel = Random.Range(1, levels.Length + 1);

            }

        }

        ResetValues();
        CreateLevel(levels[currentLevel - 1]);

    }
    private void ResetValues()
    {
        player.position = Vector3.zero;
        Destroy(currentLevelPrefab);
        currentLevelPrefab = new GameObject("LevelPrefabs");
        prevPitLength = 0;
        prevRoadLength = 0;
    }


    private void CreateLevel(Level level)
    {
        
        level.groundMat.color = level.groundColor;
        level.pitMat.color = level.pitColor;
        for (int i = 0; i < level.roads.Length; i++)
        {
            //Setting up our road
            plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.SetParent(currentLevelPrefab.transform);
            plane.GetComponent<Renderer>().material = level.groundMat;
            //Setting up pit
            GameObject pitPlatform = Instantiate(downPlatform);
            pitPlatform.transform.SetParent(currentLevelPrefab.transform);
            pit = pitPlatform.transform.GetChild(0).gameObject;
            pit.GetComponent<Renderer>().material = level.pitMat;
            pit.GetComponent<ObjectPit>().neededAmount = level.objectPoolNeededAmount[i];
            pit.GetComponent<ObjectPit>().groundMat = level.groundMat;

            float roadLength = level.roads[i].roadLength;
            plane.transform.localScale = new Vector3(1, 1, roadLength / 10);
            plane.transform.position = new Vector3(0, 0, prevRoadLength + prevPitLength+
                roadLength/ 2);
            pitPlatform.transform.position = new Vector3(0,0,roadLength +prevRoadLength+prevPitLength+ defaultPitLength/2);
            prevRoadLength+= roadLength;
            prevPitLength+= defaultPitLength;

        }
        CreateFinishLine(level.groundMat);
        
    }
    private void CreateFinishLine(Material groundMat)
    {
        GameObject end = Instantiate(finishPlatform);
        end.transform.SetParent(currentLevelPrefab.transform);
        end.GetComponent<Renderer>().material = groundMat;
        end.transform.position= new Vector3(0, 0, prevRoadLength + prevPitLength +
                end.transform.localScale.z*5);
    }
}
