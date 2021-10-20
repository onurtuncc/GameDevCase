using UnityEngine;
using Lean.Pool;


    public class LevelCreator : MonoBehaviour
    {
        #region Needed prefabs
        public GameObject downPlatform;
        public GameObject finishPlatform;
        public GameObject rampPlatform;
        public GameObject defaultPlane;
        public GameObject defaultCube;
        public GameObject defaultCylinder;
        public GameObject defaultSphere;
        public GameObject barrierCube;
        #endregion

        #region Primitive objects

        private GameObject pit;
        private Material levelMat;
        private Color levelColor;
        private GameObject nextLevelPrefab;
        private GameObject currentLevelPrefab;
        #endregion

        #region Length values to calculate positions
        private float roadHorizontalLength = 10;
        private float prevRoadLength = 0;
        private float prevPitLength = 0;
        private float barrierLength = 0;
        #endregion

        #region Default values
        private float defaultPitLength = 10f;
        private float defaultCollectableMass = 0.5f;
        private float endPosZ = 0;
        private float startPosZ = 0;
        #endregion

        #region Main method for creating level
        public void CreateLevel(Level level, bool isReplay)
        {

            levelMat = level.groundMat;
            levelColor = level.groundColor;
            if (currentLevelPrefab != null)
            {
                foreach (ObjectPool go in currentLevelPrefab.GetComponentsInChildren<ObjectPool>())
                {

                    LeanPool.Despawn(go);
                }

            }

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
            if (currentLevelPrefab != null) currentLevelPrefab.transform.position = Vector3.zero;
            prevPitLength = 0;
            prevRoadLength = 0;

            for (int i = 0; i < level.roads.Length; i++)
            {
                //Setting up our road
                //plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

                var plane = SpawnObject(defaultPlane, parentObject);
                //if (!addToPool) LeanPool.Detach(plane.transform);
                SetMatAndColor(plane.GetComponent<Renderer>());
                //Setting up pit
                GameObject pitPlatform = Instantiate(downPlatform, parentObject.transform);

                pit = pitPlatform.transform.GetChild(0).gameObject;
                pit.GetComponent<Renderer>().material = level.pitMat;
                pit.GetComponent<Renderer>().material.color = level.pitColor;
                pit.GetComponent<ObjectPit>().neededAmount = level.objectPoolNeededAmount[i];
                pit.GetComponent<ObjectPit>().groundColor = level.groundColor;
                pit.GetComponent<ObjectPit>().groundMaterial = level.groundMat;
                //Setting up positions and scales
                float roadLength = level.roads[i].roadLength;
                plane.transform.localScale = new Vector3(1, 1, roadLength / 10);
                plane.transform.position = new Vector3(0, 0, startPosZ + prevRoadLength + prevPitLength +
                    roadLength / 2);
                pitPlatform.transform.position = new Vector3(0, 0, startPosZ + roadLength + prevRoadLength + prevPitLength + defaultPitLength / 2);
                //Creating objects on the road
                
                //Updating values
                prevRoadLength += roadLength;
                prevPitLength += defaultPitLength;

            }
            CreateObjectsOnRoad(level, parentObject);
            barrierLength = CreateFinishLine(level.groundColor, level.groundMat, parentObject, isReplay, level.levelEndType);
            CreateSideBarriers(parentObject, isReplay);
        }
        #endregion

        #region Create Methods For Level Elements
        private void SetMatAndColor(Renderer go)
        {
            go.material = levelMat;
            go.material.color = levelColor;
        }
        private void CreateSideBarriers(GameObject parentObject, bool isReplay)
        {

            var sideBarrier = SpawnObject(barrierCube, parentObject);
            float posX = (roadHorizontalLength + sideBarrier.transform.localScale.x) / 2;
            var scale = sideBarrier.transform.localScale;
            sideBarrier.transform.localScale = new Vector3(scale.x, scale.y + 1, barrierLength);
            sideBarrier.transform.position = new Vector3(posX, sideBarrier.transform.position.y, barrierLength / 2 + startPosZ);
            var sideBarrierLeft = SpawnObject(barrierCube, parentObject);
            sideBarrierLeft.transform.position = new Vector3(-posX, sideBarrier.transform.position.y, barrierLength / 2 + startPosZ);
            sideBarrierLeft.transform.localScale = new Vector3(scale.x, scale.y + 1, barrierLength);



        }

        private void CreateObjectsOnRoad(Level level, GameObject parentObject)
        {
            var collectables = level.collectableobjects;
            GameObject collectableElement;
            for (int i = 0; i < collectables.Length; i++)
            {
                var collectable = collectables[i];
                switch (collectable.objectType)
                {
                    case (Collectableobject.ObjectType.Cube):
                        collectableElement = SpawnObject(defaultCube, parentObject);
                        break;
                    case (Collectableobject.ObjectType.Cylinder):
                        collectableElement = SpawnObject(defaultCylinder, parentObject);
                        break;
                    case (Collectableobject.ObjectType.Sphere):
                        collectableElement = SpawnObject(defaultSphere, parentObject);
                        break;
                    default:
                        collectableElement = SpawnObject(defaultSphere, parentObject);
                        break;
                }

                collectableElement.transform.rotation = Quaternion.Euler(0, 0, 0);
                collectableElement.transform.localScale *= collectable.size;
                collectableElement.transform.position = collectable.spawnPoint + Vector3.up * collectable.size + Vector3.forward * startPosZ;
                //collectableElement.GetComponent<Rigidbody>().isKinematic = false;
                //AddCollectableFeatures(collectableElement);
            }
        }
        private void AddCollectableFeatures(GameObject collectableObject)
        {
            if (collectableObject.activeSelf == true)
            {
                if (collectableObject.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody rb = collectableObject.AddComponent<Rigidbody>();
                    rb.mass = defaultCollectableMass;
                    collectableObject.tag = "Collectable";
                }

            }


        }

        private float CreateFinishLine(Color groundColor, Material groundMat, GameObject parentObject, bool isReplay, Level.LevelEndType levelEndType)
        {
            GameObject end;
            GameObject planeObject;
            float endLength;
            switch (levelEndType)
            {
                case (Level.LevelEndType.Ramp):
                    end = Instantiate(rampPlatform);
                    planeObject = end.transform.GetChild(0).gameObject;
                    endLength = planeObject.transform.localScale.z * 10;
                    break;
                default:
                    end = Instantiate(finishPlatform);
                    planeObject = end;
                    endLength = end.transform.localScale.z * 10;
                    break;
            }
            SetMatAndColor(planeObject.GetComponent<Renderer>());


            end.transform.SetParent(parentObject.transform);


            end.transform.position = new Vector3(0, 0, startPosZ + prevRoadLength + prevPitLength +
                   endLength / 2);
            if (!isReplay)
            {
                endPosZ = prevRoadLength + prevPitLength +
                    endLength;
            }
            return prevRoadLength + prevPitLength +
                   endLength;


        }
        #endregion

        private GameObject SpawnObject(GameObject gameObjectToSpawn, GameObject parentObject)
        {
            return LeanPool.Spawn(gameObjectToSpawn, parentObject.transform);
        }
    }
