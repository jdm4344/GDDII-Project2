using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour {

    public int width = 0;
    public int height = 0;
    public char[,] dataGrid;
    public List<GameObject> blockList;
    public GameObject grass;
	public GameObject dirt;

    //access to the game manager
    GameManager gameManager;

	// Use this for initialization
	void Start () {
        //width = GameObject.Find("GameManager").GetComponent<GameManager>().width;
        //height = GameObject.Find("GameManager").GetComponent<GameManager>().height;
        //dataGrid = new char[width, height];
        //grid = new GameObject[width, height];

        // hold here until the width and height has been set
        //while (width != 0 && height != 0)
        //{
        //	Debug.Log("waiting for height and width to be set");
        //}

        gameManager = GameObject.Find("GameManager_Empty").GetComponent<GameManager>();
        width = gameManager.width;
        height = gameManager.height;
        dataGrid = gameManager.gridArray;
        
		for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
				switch (dataGrid[i, j])
				{
					case 'g':
						blockList.Add(Instantiate(grass, new Vector3(j + 0.5f, i + 0.5f, 0), Quaternion.identity));
						//Debug.Log("instantiated grass block");
						break;
					case 'd':
						blockList.Add(Instantiate(dirt, new Vector3(j + 0.5f, i + 0.5f, 0), Quaternion.identity));
						//Debug.Log("instantiated dirt block");
						break;
					default:
						break;
				}
            }
        }

        //Debug.Log("skipped instantiating the board");
	}
	
	// Update is called once per frame
	void Update () {
        //CheckMouseOver();
	}

    //Check to see if the mouse cursor is over a tile
    void CheckMouseOver()
    {
        for (int i = 0; i < blockList.Count; i++)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Collider tileCollider = blockList[i].GetComponent<Collider>();
            RaycastHit hit;

            if (tileCollider.Raycast(ray, out hit, 10.0f))
            {
                blockList[i].GetComponent<Material>().SetColor("_EmissionColor", new Color(89, 89, 89));
            }

            else
            {
                blockList[i].GetComponent<Material>().SetColor("_EmissionColor", new Color(0, 0, 0));
            }
        }
    }
}
