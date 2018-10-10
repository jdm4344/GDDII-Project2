using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Class that handles level generation, regulates other managers (except sceneManager), and deals with
/// other general realtime events required for gameeplay.
/// </summary>
public class GameManager : MonoBehaviour {

    // Management Variables
    
    #region External Managers

        // Access to set everything
        public EnemyManager enemyManager;

    #endregion

    #region Map Generation

        // Map Height & Width (Read from file)
        public int height;
        public int width;
        // char grid representation of the field
        char[,] gridArray;
        // char list representation of the enemies to spawn
        char[] enemySpawnList;
        // max enemies on the field
        int maxEnemies;
        // spawn cooldown
        int spawnCooldown;
        // X spawn pos
        int xSpawnPos;
        // Y spawn pos
        int ySpawnPos;

    #endregion

    #region Map Regulation

        private bool tileSnap = true;
        public bool TileSnap {
            get { return tileSnap; }
        }

    #endregion

    #region Game Variables

        private bool gameStarted = false;
        private bool isGameActive = false;

    #endregion

    // Initialization
    void Start () {
        ReadLevelData();
        SetLevelData();
	}
	
	// Update is called once per frame
	void Update () {
		
        // Menu and pausing
        if (Input.GetButtonDown("Esc")) {
            isGameActive = !isGameActive;
            if (isGameActive)
                Debug.Log("Game is Active");
            else
                Debug.Log("Game is Inactive");
        }

        


        if (isGameActive) {



        }
	}

    // Called every fixed framerate frame
    void FixedUpdate() {

    }

    // sets all data into the other classes
    void SetLevelData() {
        // set up the spawn queue
        for (int i = 0; i < enemySpawnList.Length; i++)
        {
            // add the appropriate enemy type
            switch (enemySpawnList[i])
            {
                case 'v':
                    enemyManager.enemySpawnQueue.Add(new Vampire());
                    break;
                default:
                    break;
            }
        }

        // set other attributes
        enemyManager.maxEnemies = maxEnemies;
        enemyManager.spawnCooldown = spawnCooldown;
        enemyManager.spawnPoint = new Vector3(xSpawnPos + .5f, ySpawnPos + .5f, 0);
    }

    // reads in level data and sets it all
    void ReadLevelData() {
        StreamReader reader = new StreamReader("levelInfo.txt");

        string data;
        // get the height and width and array of blocks
        data = reader.ReadLine();
        height = System.Convert.ToInt32(data);
        data = reader.ReadLine();
        width = System.Convert.ToInt32(data);
        // make the array
        gridArray = new char[width, height];

        // get each line that is the grid data
        for (int i = 0; i < height; i++)
        {
            data = reader.ReadLine();
            // put the string data into the array representation
            for (int j = 0; j < width; j++)
            {
                gridArray[i, j] = data[j];
            }
        }

        // get enemy spawn list
        data = reader.ReadLine();
        enemySpawnList = new char[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            enemySpawnList[i] = data[i];
        }

        // max enemies
        data = reader.ReadLine();
        maxEnemies = System.Convert.ToInt32(data);

        // spawn cooldown
        data = reader.ReadLine();
        spawnCooldown = System.Convert.ToInt32(data);

        // spawn pos X
        data = reader.ReadLine();
        xSpawnPos = System.Convert.ToInt32(data);

        // spawn pos Y
        data = reader.ReadLine();
        ySpawnPos = System.Convert.ToInt32(data);

        // close file
        reader.Close();
    }
}
