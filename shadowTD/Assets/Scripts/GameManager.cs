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

    // access to set everything
    public EnemyManager enemyManager;
    // game grid prefab
    public GameObject gameGridPrefab;

    // base prefab
    public GameObject baseTentPrefab;
    
    // enemy prefab types
	public Vampire vampirePrefab;

    //scene management
    public GameObject sceneChanger;

    #endregion

    #region Map Generation

    // map height & width (read from file)
    public int height;
    public int width;

    // 2D char array of every type of terrain for the grid
    public char[,] gridArray;

    // how many enemies are in the largest wave
    public int waveSize;

    // number of waves
    public int waves;

    // char array of the enemies to spawn
    public char[,] enemySpawnArray;

    // max enemies on the field
    public int maxEnemies;

    // cooldown between individual enemy spawns
    public float spawnCooldown;

    // spawn pos
    public int xSpawnPos;
    public int ySpawnPos;

    // 2D char array of where towers are on the grid
    public char[,] towerArray;

    // 
    private bool mapRead = true;

    #endregion

    #region Map Regulation

    // getter and setter for tileSnap
    private bool tileSnap = true;
    public bool TileSnap
    {
        get { return tileSnap; }
    }

    #endregion

    #region Game Variables

    public bool victory;
    
    // costs and purchasing
    public int funds;

    // game state
    private bool gameStarted;
    private bool paused;

    #endregion

    #region GUI Variables

    public GameObject Overlay;
    public Transform textBox;


	public CursorMode cursorMode = CursorMode.Auto;
	public Texture2D defaultCursor;
	private Vector2 hotSpot = new Vector2(10.0f, 6.0f);

    #endregion

    Camera mainCamera;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        Overlay = GameObject.Find("Overlay (In-Game)");
        mainCamera = Camera.main;
    }

    // Initialization
    void Start () {
        ReadLevelData();
        SetEnemyManagerData();

        Instantiate(gameGridPrefab, Vector3.zero, Quaternion.identity);


        // hard coded base location
        Instantiate(baseTentPrefab, new Vector3(19.0f + .5f, 4.0f + .5f, -0.3f), Quaternion.identity);

        SetupCamera();

        funds = 40;
	}
	
	// Update is called once per frame
	void Update ()
    {
        baseTentPrefab = GameObject.Find("BaseTentPrefab(Clone)");
        //If base health falls to zero, you lose
        if (baseTentPrefab.GetComponent<BaseTent>().health <= 0)
        {
            Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);
            sceneChanger.GetComponent<SceneChange>().EndGame2();
        }

    }

    // called when an enemy is killed
    public void AddFunds(int income)
    {
        funds += income;
    }

    // sets attributes in the enemy manager
    void SetEnemyManagerData()
    {
        if (mapRead)
        {
            // set attributes
            enemyManager.waves = waves;
            enemyManager.waveSize = waveSize;
            enemyManager.enemySpawnArray = enemySpawnArray;
            enemyManager.maxEnemies = maxEnemies;
            enemyManager.spawnCooldown = spawnCooldown;
            enemyManager.spawnPoint = new Vector3(xSpawnPos + .5f, ySpawnPos + .5f, 0);
        }
    }

    // reads in level data and saves it all
    void ReadLevelData()
    {
        try
        {
            StreamReader reader = new StreamReader("Assets/levelInfo.txt");

            // temporary buffer
            string data;

            // get the height and width and array of blocks
            data = reader.ReadLine();
            height = System.Convert.ToInt32(data);
            data = reader.ReadLine();
            width = System.Convert.ToInt32(data);

            // make the array
            gridArray = new char[height, width];

            // get each line that is the grid data
            for (int i = 0; i < height; i++)
            {
                data = reader.ReadLine();

                // put the string data into the array representation
                for (int j = 0; j < width; j++)
                {
					gridArray[i, j] = data[j];
                    string output = i.ToString() + " " + j.ToString();
                    //Debug.Log(output);
                }
            }

            // get the max wave size
            data = reader.ReadLine();
            waveSize = System.Convert.ToInt32(data);

            // get how many waves there are
            data = reader.ReadLine();
            waves = System.Convert.ToInt32(data);

            // make the array
            enemySpawnArray = new char[waves, waveSize];

            // get each line that is enemy wave spawn data
            for (int i = 0; i < waves; i++)
            {
                data = reader.ReadLine();

                // put the string data into the array representation
                for (int j = 0; j < data.Length; j++)
                {
                    enemySpawnArray[i, j] = data[j];
                }
            }

            // max enemies on screen at once
            data = reader.ReadLine();
            maxEnemies = System.Convert.ToInt32(data);

            // spawn cooldown between enemies
            data = reader.ReadLine();
            spawnCooldown = (float)System.Convert.ToDouble(data);

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
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("Error with reading level data: \n" + e);
        }
    }

    // initial position of the main camera
    void SetupCamera()
    {
        mainCamera.transform.position = new Vector3(width / 2, (height / 2) - 3, -5);
        mainCamera.transform.Rotate(-25.0f, 0, 0);
        mainCamera.GetComponent<CameraMovement>().initPosition();
    }
}
