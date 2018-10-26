using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Vector3 position;
    private int width;
    private int height;

	// Use this for initialization
	void Start () {
        
	}

    public void initPosition()
    {
        position = transform.position;
        width = GameObject.Find("GameManager").GetComponent<GameManager>().width;
        height = GameObject.Find("GameManager").GetComponent<GameManager>().height;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            if(position.x > 4f)
            {
                position.x -= 1.5f * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (position.x < width - 4f)
            {
                position.x += 1.5f * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (position.y > 0f)
            {
                position.y -= 1.5f * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (position.y < height - 6f)
            {
                position.y += 1.5f * Time.deltaTime;
            }
        }
        transform.position = position;
    }
}
