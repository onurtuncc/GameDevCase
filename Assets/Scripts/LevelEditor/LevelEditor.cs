using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] TMP_InputField roadLengthInput;
    [SerializeField] TMP_InputField neededAmountInput;
    [SerializeField] TMP_InputField objectSizeInput;
    [SerializeField] GameObject pitPlatformPrefab;
    private List<GameObject> objectsCreated = new List<GameObject>();
    private List<GameObject> collectableObjectsCreated = new List<GameObject>();
    private List<float> objectsLength=new List<float>();
    private List<int> neededAmounts = new List<int>();
    private List<Road> roads = new List<Road>();
    private List<Collectableobject> collectables = new List<Collectableobject>();
    private float endPosZ=0;
    private float pitLength = 10f;
    [SerializeField] Level level;
    //Last object type: 0-Road 1- Collectable 2-Pit
    private List<int> lastobjectType = new List<int>();
    

    private void Awake()
    {
        level = ScriptableObject.CreateInstance<Level>();
        level.name = "Level to Save";
    }
    public void CreateCollectable(int objectType)
    {
        float size=0f;
        if (float.TryParse(objectSizeInput.text, out size))
        {
            if (size <= 0)
            {
                size = 1;
            }


            GameObject collectableObjectGo;
            switch (objectType)
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
            collectableObjectsCreated.Add(collectableObjectGo);
            Collectableobject.ObjectType objectType1 = (Collectableobject.ObjectType)objectType;
            Collectableobject collectableobject = new Collectableobject(objectType1, size);
            collectables.Add(collectableobject);
            lastobjectType.Add(1);
        }

    }
    public void CreateRoad(float roadLength)
    {
        if(float.TryParse(roadLengthInput.text,out roadLength)){

            if (roadLength > 0)
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
                
            }
        }

    }
    public void CreatePit(int neededAmount)
    {
        if(int.TryParse(neededAmountInput.text,out neededAmount))
        {
            if (neededAmount > 0)
            {
                var pitPlatform = Instantiate(pitPlatformPrefab);
                var pit = pitPlatform.transform.GetChild(0).GetComponent<ObjectPit>();
                pit.neededAmount = neededAmount;
                pitPlatform.transform.position = new Vector3(0, 0, endPosZ + pitLength/2);
                endPosZ += pitLength;
                objectsCreated.Add(pitPlatform);
                objectsLength.Add(pitLength);
                lastobjectType.Add(2);
                neededAmounts.Add(neededAmount);
               
            }
        }
        
    }
    
    
    public void SetSpawnPoints()
    {
        for(int i = 0; i < collectables.Count; i++)
        {
            collectables[i].spawnPoint = collectableObjectsCreated[i].transform.position;
        }
        
        
    }
    public void SaveLevel()
    {
        SetSpawnPoints();
        level.roads = roads.ToArray();
        level.objectPoolNeededAmount = neededAmounts.ToArray();
        level.collectableobjects = collectables.ToArray();
        


    }
    public void DeleteLastObject()
    {
        
        if (objectsCreated.Count > 0 || collectableObjectsCreated.Count>0)
        {
            int lastType = lastobjectType[lastobjectType.Count - 1];
            lastobjectType.RemoveAt(lastobjectType.Count - 1);
            if (lastType == 0)
            {
                RemoveAndDestroy(objectsCreated);
                roads.RemoveAt(roads.Count - 1);
                RemoveAndDelete(objectsLength);
            }
            else if(lastType == 2)
            {
                RemoveAndDestroy(objectsCreated);
                RemoveAndDelete(objectsLength);
                neededAmounts.RemoveAt(neededAmounts.Count - 1);
            }
            else if(lastType == 1)
            {
                RemoveAndDestroy(collectableObjectsCreated);
                collectables.RemoveAt(collectables.Count - 1);
            }
            
            
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
}
