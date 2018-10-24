using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour {

    // Attributes

    // Managers
    GameManager gameManager;
    private GUIManager guiManager;
    private tempTowerManager towerManager;

    // the grid
    public int width = 0;
    public int height = 0;
    public char[,] dataGrid;
    public List<GameObject> blockList; // Keeps track of terrain grid objects
    public List<GameObject> turretList; // Keeps track of turret objects
    public char[] turretTypes; // Keeps track of turret types at selectedIndex location
    public GameObject selectedTile; // Terrain object that is currently moused-over
    public int selectedIndex; // Index of selectedTile in blockList
    private bool cancelPlacement;
    // Prefabs
    public GameObject grass;
    public GameObject dirt;
    public GameObject water;
    public GameObject baseTent;

    // Use this for initialization
    void Start ()
    {
        guiManager = GameObject.FindGameObjectWithTag("InGameOverlay").GetComponent<GUIManager>();
        guiManager.gameGrid = this;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        towerManager = gameManager.GetComponent<tempTowerManager>();
        //towerManager.gameGrid = this;
        selectedTile = null;
        width = gameManager.width;
        height = gameManager.height;
        dataGrid = gameManager.gridArray;
        cancelPlacement = false;
        turretList = new List<GameObject>();
        
		for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
				switch (dataGrid[i, j])
				{
					case 'g':
						blockList.Add(Instantiate(grass, new Vector3(j + 0.5f, i + 0.5f, 0), Quaternion.identity));
                        blockList[blockList.Count - 1].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                        break;
					case 'd':
						blockList.Add(Instantiate(dirt, new Vector3(j + 0.5f, i + 0.5f, 0), Quaternion.identity));
                        blockList[blockList.Count - 1].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                        break;
					default:
						break;
				}

                // Populate turretList with empty indices
                turretList.Add(null);
            }
        }

        turretTypes = new char[turretList.Count];

        for (int i = 0; i < turretTypes.Length; i++)
        {
            turretTypes[i] = 'e'; // 'e' for empty position
        }
	}
	
	// Update is called once per frame
	void Update () {
        CheckMouseOver();
        CheckMouseClick();

        if (cancelPlacement) {
            guiManager.buyingMachineGunNest = false;
            cancelPlacement = false;
        }
	}

    // Check to see if the mouse cursor is over a tile
    void CheckMouseOver()
    {
        bool selected = false;
        for (int i = 0; i < blockList.Count; i++)
        {
            blockList[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0));
        }
        List<GameObject> rayCastList = new List<GameObject>();
        List<int> indexList = new List<int>();
        for (int i = 0; i < blockList.Count; i++)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Collider tileCollider = blockList[i].GetComponent<Collider>();
            RaycastHit hit;

            if (tileCollider.Raycast(ray, out hit, 10.0f) && !guiManager.CursorOnUI)
            {
                /*if (guiManager.buyingMachineGunNest && !cancelPlacement)
                {
                    blockList[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0f, 0.2f, 0.8f)); // Can change this to like a transparent version of whatever asset we have for the turret
                    selected = true;
                    selectedTile = blockList[i];
                    selectedIndex = i;
                }
                else // Default yellow highlight
                {
                    blockList[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.6f, 0.6f, 0.6f));
                    selected = true;
                    selectedTile = blockList[i];
                    selectedIndex = i;
                }*/
                rayCastList.Add(blockList[i]);
                indexList.Add(i);
            }
            else
            {
                blockList[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0));
            }
        }
        GameObject selectedObj = null;
        int selectIndex = 0;
        for(int i = 0; i < rayCastList.Count; i++)
        {
            float distance = 10000.0f;

            if(rayCastList[i].transform.position.y < distance)//Mathf.Abs(Vector3.Magnitude(rayCastList[i].transform.position - transform.position)) < distance)
            {
                selectedObj = rayCastList[i];
                selectIndex = indexList[i];
            }
        }
        if (guiManager.buyingMachineGunNest && !cancelPlacement)
        {
            selectedObj.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0f, 0.2f, 0.8f)); // Can change this to like a transparent version of whatever asset we have for the turret
            selected = true;
            selectedTile = blockList[selectIndex];
            selectedIndex = selectIndex;
        }
        else // Default yellow highlight
        {
            blockList[selectIndex].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.6f, 0.6f, 0.6f));
            selected = true;
            selectedTile = blockList[selectIndex];
            selectedIndex = selectIndex;
        }
        if (!selected)
        {
            selectedTile = null;
        }
    }

    void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0) && selectedTile != null) // Check for left-click and that a tile is highlighted
        {
            if(guiManager.deleteState == true)
            {
                Debug.Log("deleteState = " + guiManager.deleteState + " checkMouseClick");

                GameObject.Destroy(turretList[selectedIndex]);
                turretList.RemoveAt(selectedIndex);
                turretTypes[selectedIndex] = 'e';

                guiManager.deleteState = false;
                return;
            }
            else if (guiManager.buyingMachineGunNest && turretTypes[selectedIndex] == 'e') 
            {
                GameObject newTurret = Instantiate(towerManager.machineGunPrefab, new Vector3(selectedTile.transform.position.x, selectedTile.transform.position.y, -1), Quaternion.identity);
                towerManager.towerList.Add(newTurret);
                turretList[selectedIndex] = newTurret;
                turretTypes[selectedIndex] = 't';
                gameManager.funds -= Prices.MachineGunNest;
                if (gameManager.funds < Prices.MachineGunNest) {
                    guiManager.buyingMachineGunNest = false;
                }
                
            }
        }
        if (Input.GetMouseButtonDown(1))
        {   
            // Debug.Log("Cancel");
            cancelPlacement = true;
        }
        if (selectedTile != null && Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Click");
            //towerManager.towerList.Add(Instantiate(towerManager.machineGunPrefab, new Vector3(selectedTile.transform.position.x, selectedTile.transform.position.y, -1), Quaternion.identity));
        }
    }

    public void RemoveTower()
    {
        
    }
}
