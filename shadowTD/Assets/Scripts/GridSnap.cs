using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for snaping tiles or static objects to closest integer locations. This will
/// keep us from having to perfectly place every tile/object before or during the game.
/// </summary>
public class GridSnap : MonoBehaviour {

	private GameManager gm;
	private Vector3 snapScale;
	private bool snap = true;
	private uint offSetX = 1;
	private uint offSetY = 0;

	// Set Scale and GameManager Reference (Probably will seek this info from a grid manager class)
	void Awake () {
		snapScale = gameObject.transform.localScale;
		try 
		{
			gm = GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameManager>();
			snap = gm.TileSnap;
		} 
		catch (MissingReferenceException e) 
		{
			Debug.Log("Game Manager Not Found: \n" + e);
		}
	}

	// Initializations
	void Start () {
        if (snap) 
		{
            Vector3 newVector = GetSnapPos(new Vector3());
            transform.position = newVector;
        }
	}

    // Move object position to closest integer
    Vector3 GetSnapPos(Vector3 newVector)
    {
        if (transform.position.x % snapScale.x < snapScale.x / 2 + offSetX) 
        {
			if (transform.position.x >= 0) {
            	newVector.x = offSetX + Mathf.FloorToInt(transform.position.x) - (Mathf.FloorToInt(transform.position.x) % snapScale.x);
			} else {
				newVector.x = offSetX + Mathf.FloorToInt(transform.position.x) - Mathf.Abs((Mathf.FloorToInt(transform.position.x) % snapScale.x));
			}
		}
        else
        {
            newVector.x = offSetX + Mathf.FloorToInt(transform.position.x + snapScale.x) - (Mathf.FloorToInt(transform.position.x) % snapScale.x);
        }

        if (transform.position.y % snapScale.y < snapScale.y / 2) 
        {
            newVector.y = offSetY + Mathf.FloorToInt(transform.position.y) - (Mathf.FloorToInt(transform.position.y) % snapScale.y);
		}
        else
        {
            newVector.y = offSetY + Mathf.FloorToInt(transform.position.y + snapScale.y) - (Mathf.FloorToInt(transform.position.y) % snapScale.y);
        }

        return newVector;
    }
}
