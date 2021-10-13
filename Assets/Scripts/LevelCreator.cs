using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{

    public GameObject downPlatform;
    public GameObject finishPlatform;

    private GameObject plane;
    private GameObject pit;

    private GameObject nextLevelPrefab;
    private GameObject currentLevelPrefab;

    private float roadHorizontalLength = 10;
    private float prevRoadLength = 0;
    private float prevPitLength = 0;
    private float barrierLength = 0;
    

    private float defaultPitLength = 10f;
    private float defaultCollectableMass = 0.5f;
    private float endPosZ=0;
    private float startPosZ = 0;

    
    
    public void CreateLevel(Level level,bool isReplay)
    {
        
        Destroy(currentLevelPrefab);
        var parentObject = new GameObject("Level Elements");
        parentObject.transform.position += Vector3.forward * endPosZ;
        if (isReplay)
        {
           
            startPosZ = 0;
            currentLevelPrefab = parentObject;

        }
        else
        {
            startPosZ = endPosZ;
            currentLevelPrefab = nextLevelPrefab;
            nextLevelPrefab = parentObject;
            
        }
        if(currentLevelPrefab!=null)currentLevelPrefab.transform.position = Vector3.zero;
        prevPitLength = 0;
        prevRoadLength = 0;
       
        for (int i = 0; i < level.roads.Length; i++)
        {
            //Setting up our road
            plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.SetParent(parentObject.transform);
            plane.GetComponent<Renderer>().material.color = level.groundColor;
            //Setting up pit
            GameObject pitPlatform = Instantiate(downPlatform);
            pitPlatform.transform.SetParent(parentObject.transform);
            pit = pitPlatform.transform.GetChild(0).gameObject;
            pit.GetComponent<Renderer>().material.color = level.pitColor;
            pit.GetComponent<ObjectPit>().neededAmount = level.objectPoolNeededAmount[i];
            pit.GetComponent<ObjectPit>().groundColor = level.groundColor;
            //Setting up positions and scales
            float roadLength = level.roads[i].roadLength;
            plane.transform.localScale = new Vector3(1, 1, roadLength / 10);
            plane.transform.position = new Vector3(0, 0, startPosZ + prevRoadLength + prevPitLength +
                roadLength / 2);
            pitPlatform.transform.position = new Vector3(0, 0, startPosZ+roadLength + prevRoadLength + prevPitLength + defaultPitLength / 2);
            //Creating objects on the road
            CreateObjectsOnRoad(level.roads[i],parentObject);
            //Updating values
            prevRoadLength += roadLength;
            prevPitLength += defaultPitLength;

        }
        
        barrierLength= CreateFinishLine(level.groundColor,parentObject,isReplay);
        CreateSideBarriers(parentObject,isReplay);
        


    }
    

    private void CreateSideBarriers(GameObject parentObject,bool isReplay)
    {
       
        var sideBarrier = GameObject.CreatePrimitive(PrimitiveType.Cube);
        float posX = (roadHorizontalLength+ sideBarrier.transform.localScale.x)/2;
        var scale = sideBarrier.transform.localScale;
        sideBarrier.transform.localScale = new Vector3(scale.x, scale.y+1, barrierLength);
        sideBarrier.transform.position = new Vector3(posX, sideBarrier.transform.position.y, barrierLength / 2+startPosZ);
        var sideBarrierLeft = Instantiate(sideBarrier);
        sideBarrierLeft.transform.position = new Vector3(-posX, sideBarrier.transform.position.y, barrierLength / 2 + startPosZ);
        sideBarrier.transform.SetParent(parentObject.transform);
        sideBarrierLeft.transform.SetParent(parentObject.transform);



    }

    private void CreateObjectsOnRoad(Road road,GameObject parentObject)
    {
        var collectables = road.collectableObjectsOnRoad;
        GameObject collectableElement;
        for (int i = 0; i < collectables.Length; i++)
        {
            var collectable = collectables[i];
            switch (collectable.objectType)
            {
                case (Collectableobject.ObjectType.Cube):
                    collectableElement = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    break;
                case (Collectableobject.ObjectType.Cylinder):
                    collectableElement = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    break;
                case (Collectableobject.ObjectType.Sphere):
                    collectableElement = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    break;
                default:
                    collectableElement = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    break;
            }
            collectableElement.transform.localScale *= collectable.size;
            collectableElement.transform.position = collectable.spawnPoint + Vector3.up * collectable.size + Vector3.forward* startPosZ;
            collectableElement.transform.SetParent(parentObject.transform);
            AddCollectableFeatures(collectableElement);
        }
    }
    private void AddCollectableFeatures(GameObject collectableObject)
    {
        Rigidbody rb = collectableObject.AddComponent<Rigidbody>();
        rb.mass = defaultCollectableMass;
        collectableObject.tag = "Collectable";

    }
    private float CreateFinishLine(Color groundColor,GameObject parentObject,bool isReplay)
    {
        GameObject end = Instantiate(finishPlatform);
        end.transform.SetParent(parentObject.transform);
        end.GetComponent<Renderer>().material.color = groundColor;
        
        end.transform.position = new Vector3(0, 0, startPosZ+prevRoadLength + prevPitLength +
                end.transform.localScale.z * 5);
        if (!isReplay)
        {
            endPosZ = prevRoadLength + prevPitLength +
                end.transform.localScale.z * 10;
        }
        return prevRoadLength + prevPitLength +
                end.transform.localScale.z * 10;


    }
}
