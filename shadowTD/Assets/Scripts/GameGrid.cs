using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour {

    public GameObject [,] grid = new GameObject[10,10];
    public GameObject grass;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                grid[i,j] = Instantiate(grass, new Vector3(j-5, i-5, 0), new Quaternion());
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        CheckMouseOver();
	}

    // Check to see if the mouse cursor is over a tile
    void CheckMouseOver()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
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
