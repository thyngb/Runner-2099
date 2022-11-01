using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGenerator : MonoBehaviour
{
    [Header("Camera")]
    public int WindowSize;
    public CameraScript cameraScript;

    private Vector2 startCameraPos;
    private Vector2 currentCameraPos;

    [Header("Level Generate")]

    [Tooltip("Starting X-Position For Generation, CameraPos + generationOffsetX")]
    public int generationOffsetX;

    [System.Serializable]
    public class PreGeneratedLevel
    {
        public PreGeneratedLevel(GameObject levelObject, int horizontalSize, int yLevelSpawn)
        {
            this.levelObject = levelObject;
            this.horizontalSize = horizontalSize;
            this.yLevelSpawn = yLevelSpawn;
        }

        public GameObject levelObject;
        public int horizontalSize;
        public int yLevelSpawn;
    }
    public PreGeneratedLevel[] levels;

    // Private Variables
    private float generateLevelPosX;
    private Queue<PreGeneratedLevel> levelQueue;
    private float slidingWindowMoveDiff;


    // Start is called before the first frame update
    void Start()
    {
        startCameraPos = cameraScript.gameObject.transform.position;
        currentCameraPos = startCameraPos;

        levelQueue = new Queue<PreGeneratedLevel>();

        generateLevelPosX = currentCameraPos.x + generationOffsetX;
    }

    // Update is called once per frame
    void Update()
    {
        currentCameraPos = cameraScript.gameObject.transform.position;

        #region Sliding Window
        if (WindowSize - currentCameraPos.x <= 0)
        {
            slidingWindowMoveDiff = currentCameraPos.x - startCameraPos.x;

            generateLevelPosX -= slidingWindowMoveDiff;

            cameraScript.SetPosition(startCameraPos);
            SlideLevelObjectsBack();
        }
        #endregion

        #region Semi-Procedural Levels

        float CameraRightEdge = currentCameraPos.x + (cameraScript.camSizeHorizontal / 2);

        if (generateLevelPosX < CameraRightEdge)
        {
            GenerateLevel();
            DestroyLevel();
        }

        #endregion
    }

    private void GenerateLevel()
    {
        // Choose A Level
        PreGeneratedLevel level = levels[(Random.Range(0, levels.Length))];

        // Determine Location
        Vector2 spawnLoc = new Vector2(
            generateLevelPosX + ((float)level.horizontalSize / 2),
            level.yLevelSpawn
            ); ;

        // Spawn Object
        GameObject generatedGameObject = Instantiate(level.levelObject, spawnLoc, Quaternion.identity);

        // Add Level To Queue
        levelQueue.Enqueue(new PreGeneratedLevel(generatedGameObject, level.horizontalSize, level.yLevelSpawn));

        // Update generateLevelPos
        generateLevelPosX += level.horizontalSize;
    }

    private void DestroyLevel()
    {
        PreGeneratedLevel lvlObject = levelQueue.Peek();

        // Look at current position of last object in queue
        Vector2 currSpawnLoc = lvlObject.levelObject.transform.position;

        // Check If Level is not seen in Camera
        if (currSpawnLoc.x + lvlObject.horizontalSize < currentCameraPos.x - (cameraScript.camSizeHorizontal / 4))
        {
            // Dequeue Object
            levelQueue.Dequeue();

            // Destroy Object
            Destroy(lvlObject.levelObject);
        }
    }

    private void SlideLevelObjectsBack()
    {
        Queue<PreGeneratedLevel> tmpLevelQueue = new Queue<PreGeneratedLevel>();

        foreach (PreGeneratedLevel level in levelQueue)
        {
            level.levelObject.transform.position = new Vector2(
                    level.levelObject.transform.position.x - slidingWindowMoveDiff,
                    level.levelObject.transform.position.y
                );

            tmpLevelQueue.Enqueue(level);
        }

        levelQueue.Clear();

        foreach (PreGeneratedLevel level in tmpLevelQueue)
        {
            levelQueue.Enqueue(level);
        }
    }
}
