using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Vector3 position;

	// Use this for initialization
	void Start () {
        
	}

    public void initPosition()
    {
        position = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            if(position.x > 4)
            {
                position.x -= 1 * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (position.x < 6)
            {
                position.x += 1 * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (position.y > 4)
            {
                position.y -= 1 * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (position.y < 6)
            {
                position.y += 1 * Time.deltaTime;
            }
        }
        transform.position = position;
    }
}
