using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempTowerManager : MonoBehaviour {

    // Variables
    public GameObject machineGunPrefab;
    public List<GameObject> towerList;
    //public GameGrid gameGrid;

    //void Awake()
    //{
    //    gameGrid = GameObject.Find("GameGrid").GetComponent<GameGrid>();
    //}

    // Use this for initialization
    void Start ()
    {
        //towerList.Add(Instantiate(machineGunPrefab, new Vector3(3 + .5f, 6 + .5f, -1), Quaternion.identity));
        //towerList.Add(Instantiate(machineGunPrefab, new Vector3(4 + .5f, 2 + .5f, -1), Quaternion.identity));
        //towerList.Add(Instantiate(machineGunPrefab, new Vector3(5 + .5f, 6 + .5f, -1), Quaternion.identity));
        //towerList.Add(Instantiate(machineGunPrefab, new Vector3(6 + .5f, 2 + .5f, -1), Quaternion.identity));

        //gameGrid.turretList[24] = towerList[0];
        //gameGrid.turretList[26] = towerList[1];
        //gameGrid.turretList[63] = towerList[2];
        //gameGrid.turretList[65] = towerList[3];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
