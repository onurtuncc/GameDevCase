using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] TMP_InputField roadLengthInput;
    [SerializeField] TMP_InputField neededAmountInput;
    [SerializeField] GameObject pitPlatformPrefab;
    Stack<GameObject> objectsCreated = new Stack<GameObject>();
    Stack<float> objectsLength=new Stack<float>();
    private float endPosZ=0;
    private float pitLength = 10f;
    [SerializeField] Level level;
    private List<Road> roads= new List<Road>();

    private void Awake()
    {
        level = ScriptableObject.CreateInstance<Level>();
        level.name = "Level to Save";
    }
    public void CreateRoad(float roadLength)
    {
        if(float.TryParse(roadLengthInput.text,out roadLength)){

            if (roadLength > 0)
            {
                var roadObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
                roadObject.transform.localScale = new Vector3(1, 1, roadLength / 10);
                roadObject.transform.position = new Vector3(0, 0, roadLength / 2 + endPosZ);
                Road road = new Road(roadLength, null);
                endPosZ += roadLength;
                roads.Add(road);
                objectsCreated.Push(roadObject);
                objectsLength.Push(roadLength);
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
                objectsCreated.Push(pitPlatform);
                objectsLength.Push(pitLength);
               
            }
        }
        
    }
    public void DeleteLastObject()
    {
        if (objectsCreated.Count > 0)
        {
            Destroy(objectsCreated.Pop());
            endPosZ -= objectsLength.Pop();
        }
    }
    
    
}
