using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {

	public Button buttonRef;
	public GameObject objRef;

	// Use this for initialization
	void Start () {
		buttonRef = this.gameObject.GetComponent<Button>();
		buttonRef.onClick.AddListener(HandleElemClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void HandleElemClick () {
		Instantiate(objRef);
	}
}
