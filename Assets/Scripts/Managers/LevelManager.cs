using UnityEngine;


    public class LevelManager : MonoBehaviour
    {
       
        public Level[] levels;
        private int currentLevel;
        private int levelToCreate;
        private int levelToReplay;
        private LevelCreator levelCreator;

        private void Start()
        {
            levelCreator = GetComponent<LevelCreator>();
            CreateFirst2Level();

        }
        public void CreateFirst2Level()
        {

            var createData = PlayerPrefController.Instance.GetLevelCreateData();
            levelToReplay = createData[0];
            levelToCreate = createData[1];

            levelCreator.CreateLevel(levels[levelToReplay], false);
            levelCreator.CreateLevel(levels[levelToCreate], false);

        }

        public void PickLevelAndCreate(bool isReplay)
        {
            currentLevel = PlayerPrefController.Instance.GetCurrentLevelData();
            Debug.Log("Current level is:" + currentLevel);
            if (!isReplay)
            {

                if (currentLevel >= levels.Length)
                {
                    //Debug.Log("Random Level");
                    levelToReplay = levelToCreate;
                    levelToCreate = Random.Range(0, levels.Length);

                }
                else
                {
                    levelToCreate = currentLevel;
                    levelToReplay = levelToCreate - 1;

                }
                
                levelCreator.CreateLevel(levels[levelToCreate], isReplay);
            }
            else
            {

               
                levelCreator.CreateLevel(levels[levelToReplay], isReplay);
            }
            PlayerPrefController.Instance.SaveLevelData(levelToReplay, levelToCreate);
        }

    }
