using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour {

    public int width = 0;
    public int height = 0;
    public char[,] dataGrid;
    public GameObject[,] grid;
    public GameObject grass;
	public GameObject dirt;

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



		for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
				switch (dataGrid[i, j])
				{
					case 'g':
						grid[i, j] = Instantiate(grass, new Vector3(j - width / 2, i - height / 2, 0), Quaternion.identity);
						Debug.Log("instantiated grass block");
						break;
					case 'd':
						grid[i, j] = Instantiate(dirt, new Vector3(j - width / 2, i - height / 2, 0), Quaternion.identity);
						Debug.Log("instantiated dirt block");
						break;
					default:
						break;
				}
            }
        }

        Debug.Log("skipped instantiating the board");
	}
	
	// Update is called once per frame
	void Update () {
        //CheckMouseOver();
	}

    // Check to see if the mouse cursor is over a tile
    void CheckMouseOver()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Collider tileCollider = grid[i, j].GetComponent<Collider>();
                RaycastHit hit;

                if(tileCollider.Raycast(ray, out hit, 10.0f))
                {
                    grid[i, j].GetComponent<Material>().SetColor("_EmissionColor", new Color(89,89,89));
                }

                else
                {
                    grid[i, j].GetComponent<Material>().SetColor("_EmissionColor", new Color(0, 0, 0));
                }
            }
        }
    }
}
