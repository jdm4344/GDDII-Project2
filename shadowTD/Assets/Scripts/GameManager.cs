using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour {

    // attributes

    // height and width
    int height;
    int width;
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

    // Use this for initialization
    void Start () {
        ReadLevelData();
        SetLevelData();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // sets all data into the other classes
    void SetLevelData() {

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
