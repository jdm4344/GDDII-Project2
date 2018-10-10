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
	public GameGrid gameGrid;

	public Vampire vampire;

    #endregion

    #region Map Generation

        // Map Height & Width (Read from file)
        public int height;
        public int width;
        // char grid representation of the field
        public char[,] gridArray;
        // char list representation of the enemies to spawn
        public char[] enemySpawnList;
        // max enemies on the field
        public int maxEnemies;
        // spawn cooldown
        public int spawnCooldown;
        // X spawn pos
        public int xSpawnPos;
        // Y spawn pos
        public int ySpawnPos;
        private bool mapRead = true;

    #endregion

    #region Map Regulation

        private bool tileSnap = true;
        public bool TileSnap {
            get { return tileSnap; }
        }

    #endregion

    #region Game Variables

        private bool gameStarted = false;
        private bool isGameActive = true;
        public bool playerVictory = false;
        public bool playerDefeat = false;

        public uint difficulty = 1; // Have this here to have scaling for how much money you passively receive
        private List<Tower> towerList;
        private List<Tower> purchasableTowers;
        public int baseHealth = 100;
        public int funds = 0;
        public int moneyTickAmount = 2;
        public float moneyTick = 1.0f;
        public float curTime = 0.0f;


    #endregion

    #region GUI Variables

        // 

    #endregion

    // Initialization
    void Start () {
        ReadLevelData();
        SetLevelData();
	}
	
	// Update is called once per frame
	void Update () {
		
        // Menu and pausing
        //if (Input.GetButtonDown("Esc")) 
        //{
        //    isGameActive = !isGameActive;
        //    if (isGameActive)
        //        Debug.Log("Game is Active");
        //    else
        //        Debug.Log("Game is Inactive");
        //}

        // Most gameplay code can be handled here
        if (isGameActive) {

            // Generate funds
            AddFunds(Time.deltaTime);

            // Adding towers
            

            // Win/Lose conditions
            if (baseHealth <= 0 && baseHealth > -999) {
                playerDefeat = true;
            } else if (enemyManager.enemiesDefeated) {
                playerVictory = true;
            }

            // After action reports and possible scene transitions
            if (playerVictory) {
                Debug.Log("Victory!");
                baseHealth = -999;
                // Can talk to scene manager and switch scenes here or maybe show a report on screen of stats
            } else if (playerDefeat) {
                Debug.Log("Defeat.");
            }
        }
	}

    //
    void AddFunds(float time) {
        curTime += time;

        if (curTime >= moneyTick) {
            curTime = 0;
            funds += moneyTickAmount;
            Debug.Log(funds);
        }
    }

    // sets all data into the other classes
    void SetLevelData() {
        if (mapRead) {
            // set up the spawn queue
            for (int i = 0; i < enemySpawnList.Length; i++)
            {
                // add the appropriate enemy type
                switch (enemySpawnList[i])
                {
                    case 'v':
                        enemyManager.enemySpawnQueue.Add(vampire);
                        break;
                    default:
                        break;
                }
            }

            // set other attributes
            enemyManager.maxEnemies = maxEnemies;
            enemyManager.spawnCooldown = spawnCooldown;
            enemyManager.spawnPoint = new Vector3(xSpawnPos + .5f, ySpawnPos + .5f, 0);

			gameGrid.width = width;
			gameGrid.height = height;
			gameGrid.dataGrid = gridArray;
        }
    }

    // reads in level data and sets it all
    void ReadLevelData() {

        try {
            StreamReader reader = new StreamReader("Assets/levelInfo.txt");

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

            // successful
            mapRead = true;
        } catch (FileNotFoundException e) {
            Debug.Log("Error with reading level data: \n" + e);
        }
    }
}
