using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
using Lean.Touch;

public class LevelEditor : MonoBehaviour
{
    #region UI elements
    [SerializeField] TMP_InputField roadLengthInput;
    [SerializeField] TMP_InputField neededAmountInput;
    [SerializeField] TMP_InputField objectSizeInput;
    [SerializeField] TMP_InputField levelNameInput;
    [SerializeField] GameObject pitPlatformPrefab;
    [SerializeField] Button roadButton;
    [SerializeField] Button pitButton;
    [SerializeField] Toggle isBonus;
    #endregion

    #region Lists
    private List<GameObject> objectsCreated = new List<GameObject>();
    private List<GameObject> collectableObjectsCreated = new List<GameObject>();
    private List<float> objectsLength=new List<float>();
    private List<int> neededAmounts = new List<int>();
    private List<Road> roads = new List<Road>();
    private List<Collectableobject> collectables = new List<Collectableobject>();
    //Last object type: 0-Road 1- Collectable 2-Pit
    private List<int> lastobjectType = new List<int>();
    #endregion

    #region Variables
    private float endPosZ=0;
    private float pitLength = 10f;
    private Transform selected;
    [SerializeField] Level level;
    #endregion

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            if (Input.GetMouseButtonDown(0)&& hit.transform.tag=="Collectable") selected = hit.transform;
        }
    }

    #region Create Methods
    public void CreateCollectable(int objectType)
    {
        float size=0f;
        if (float.TryParse(objectSizeInput.text, out size))
        {
            if (size <= 0)
            {
                size = 1;
            }

            CreateCollectableObject(objectType, size);
            

        }
       

    }
    public GameObject CreateCollectableObject(int type,float size)
    {
        GameObject collectableObjectGo;

        switch (type)
        {
            case (0):
                collectableObjectGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;
            case (1):
                collectableObjectGo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;
            case (2):
                collectableObjectGo = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                break;
            default:
                collectableObjectGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;
        }
        collectableObjectGo.transform.localScale *= size;
        collectableObjectGo.transform.position += Vector3.up * size / 2;
        AddCollectableFeature(collectableObjectGo);
        Collectableobject.ObjectType objectType1 = (Collectableobject.ObjectType)type;
        Collectableobject collectableobject = new Collectableobject(objectType1, size);
        collectables.Add(collectableobject);
        return collectableObjectGo;
    }
    private void AddCollectableFeature(GameObject go)
    {
        go.tag = "Collectable";
        go.AddComponent<LeanDragTranslate>();
        go.AddComponent<MoveObject>();
        go.GetComponent<Renderer>().material.color = Color.red;
        collectableObjectsCreated.Add(go);
        objectsCreated.Add(go);
        
        lastobjectType.Add(1);
    }
    
    public void CreateRoad()
    {
        float roadLength;
        if(float.TryParse(roadLengthInput.text,out roadLength)){

            if (roadLength > 0)
            {
                CreateRoadObject(roadLength);
                roadButton.interactable = false;
                pitButton.interactable = true;

                
            }
        }

    }
    private GameObject CreateRoadObject(float roadLength)
    {
        var roadObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
        roadObject.transform.localScale = new Vector3(1, 1, roadLength / 10);
        roadObject.transform.position = new Vector3(0, 0, roadLength / 2 + endPosZ);
        Road road = new Road(roadLength);
        endPosZ += roadLength;
        roads.Add(road);
        objectsCreated.Add(roadObject);
        objectsLength.Add(roadLength);
        lastobjectType.Add(0);
        return roadObject;
    }
    public void CreatePit()
    {
        int neededAmount;
        if(int.TryParse(neededAmountInput.text,out neededAmount))
        {
            if (neededAmount > 0)
            {
                CreatePitObject(neededAmount);
                pitButton.interactable = false;
                roadButton.interactable = true;
               
            }
        }
        
    }
    private GameObject CreatePitObject(int neededAmount)
    {
        var pitPlatform = Instantiate(pitPlatformPrefab);
        var pit = pitPlatform.transform.GetChild(0).GetComponent<ObjectPit>();
        pit.neededAmount = neededAmount;
        pitPlatform.transform.position = new Vector3(0, 0, endPosZ + pitLength / 2);
        endPosZ += pitLength;
        objectsCreated.Add(pitPlatform);
        objectsLength.Add(pitLength);
        lastobjectType.Add(2);
        neededAmounts.Add(neededAmount);
        return pitPlatform;
    }

    #endregion

    #region Save Methods
    public void SetSpawnPoints()
    {
        for(int i = 0; i < collectables.Count; i++)
        {
            collectables[i].spawnPoint = collectableObjectsCreated[i].transform.position;
        }
        
        
    }
    public void SaveLevel()
    {
        level = ScriptableObject.CreateInstance<Level>();
        level.name = "Level to Save";
        if (levelNameInput.text != "")
        {
            level.name = levelNameInput.text;
        }
        if (neededAmounts.Count != roads.Count)
        {
            Debug.Log("ROAD AMOUNT AND Pit AMOUNT MUST BE SAME!!");
        }
        else
        {
            if (!isBonus.isOn) level.levelEndType = Level.LevelEndType.Ramp;
            SetSpawnPoints();
            level.roads = roads.ToArray();
            level.objectPoolNeededAmount = neededAmounts.ToArray();
            level.collectableobjects = collectables.ToArray();
            if (AssetDatabase.Contains(level)) level.name= level.name + "(1)";
            AssetDatabase.CreateAsset(level, "Assets/Resources/Game/Levels/" + level.name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = level;
        }
        



    }
    #endregion

    #region Delete Methods
    public void DeleteLastObject()
    {
        
        if (objectsCreated.Count > 0 || collectableObjectsCreated.Count>0)
        {
            int lastType = lastobjectType[lastobjectType.Count - 1];
            lastobjectType.RemoveAt(lastobjectType.Count - 1);
            if (lastType == 0)
            {
                
                roads.RemoveAt(roads.Count - 1);
                RemoveAndDelete(objectsLength);
            }
            else if(lastType == 2)
            {
                
                RemoveAndDelete(objectsLength);
                neededAmounts.RemoveAt(neededAmounts.Count - 1);
            }
            else if(lastType == 1)
            {
                collectableObjectsCreated.RemoveAt(collectables.Count - 1);
                collectables.RemoveAt(collectables.Count - 1);
            }
            RemoveAndDestroy(objectsCreated);


        }
    }
    public void RemoveAndDestroy(List<GameObject> list)
    {
        int lastIndex = list.Count - 1;
        Destroy(list[lastIndex]);
        list.RemoveAt(lastIndex);
    }
    public void RemoveAndDelete(List<float> list)
    {
        int lastIndex = list.Count - 1;
        endPosZ -= list[lastIndex];
        list.RemoveAt(lastIndex);

    }
    public void DeleteSelectedObject()
    {
        if (selected != null)
        {
            var index = collectableObjectsCreated.IndexOf(selected.gameObject);
            collectableObjectsCreated.RemoveAt(index);
            collectables.RemoveAt(index);
            var indexOfAll = objectsCreated.IndexOf(selected.gameObject);
            objectsCreated.RemoveAt(indexOfAll);
            lastobjectType.RemoveAt(indexOfAll);
            Destroy(selected.gameObject);

        }

       
    }
    #endregion

    public void LoadLevel()
    {
        string levelName;
        if (levelNameInput.text != "")
        {
            levelName = levelNameInput.text;
            Debug.Log(levelName);
            Level levelToLoad = Resources.Load<Level>("Game/Levels/" + levelName);
            Debug.Log(JsonUtility.ToJson(levelToLoad));
            levelToLoad.name = levelName;
            CreateLevel(levelToLoad);
        }
        

    }
    private void CreateLevel(Level levelToCreate)
    {
        var parentObject = new GameObject("Level Elements").transform;
        for(int i = 0; i < levelToCreate.roads.Length; i++)
        {
            CreateRoadObject(levelToCreate.roads[i].roadLength).transform.SetParent(parentObject);
            CreatePitObject(levelToCreate.objectPoolNeededAmount[i]).transform.SetParent(parentObject);
        }
        for(int i = 0; i < levelToCreate.collectableobjects.Length; i++)
        {
            var collectable = levelToCreate.collectableobjects[i];
            GameObject go=CreateCollectableObject((int)collectable.objectType,collectable.size);
            go.transform.position = collectable.spawnPoint;
            go.transform.SetParent(parentObject);
        }
        
    }
}
