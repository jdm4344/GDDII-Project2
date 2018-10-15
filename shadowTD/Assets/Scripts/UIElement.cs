using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	private GUIManager gUIManager;

	// Initialization
	void Start () {
		gUIManager = GameObject.FindGameObjectWithTag("InGameOverlay").GetComponent<GUIManager>();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		gUIManager.CursorOnUI = true;
		if (this.gameObject.GetComponent<Button>() is UnityEngine.UI.Button) { 
			Debug.Log("Type check test");
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		gUIManager.CursorOnUI = false;
	}
}
