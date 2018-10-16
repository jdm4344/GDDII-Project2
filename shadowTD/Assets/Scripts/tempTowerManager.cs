using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempTowerManager : MonoBehaviour {

    // Variables
    public GameObject machineGunPrefab;
    public List<GameObject> towerList;

	// Use this for initialization
	void Start ()
    {       
        towerList.Add(Instantiate(machineGunPrefab, new Vector3(3 + .5f, 6 + .5f, -1), Quaternion.identity));
        towerList.Add(Instantiate(machineGunPrefab, new Vector3(4 + .5f, 2 + .5f, -1), Quaternion.identity));
        towerList.Add(Instantiate(machineGunPrefab, new Vector3(5 + .5f, 6 + .5f, -1), Quaternion.identity));
        towerList.Add(Instantiate(machineGunPrefab, new Vector3(6 + .5f, 2 + .5f, -1), Quaternion.identity));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
