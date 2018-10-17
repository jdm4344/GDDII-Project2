using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	// UI Element References
	// ~ Upper Content
	public GameObject statTracker;
    public GameGrid gameGrid;
	// ~ Lower Content
	public GameObject lowerMinMaxButton;
	public GameObject removeTowerButton;
	public GameObject shop;
	public GameObject toolbar;
	private RectTransform shopRectTransf;
	private RectTransform toolbarRectTransf;

	// Screen Values
	private Resolution screenRes;

	// Management Variables

	// ~ Shop Purchase Checks
	public bool buyingMachineGunNest;
	
	// ~ UI Values
	public float padding = 0.25f;	
	public float timeToMinimize = 8.0f;
	public float transitionSpeed = 0.13f;
    public bool deleteState;

    private bool cursorOnUI;
	private bool minimized;
	private bool minmaxPress;
	private float minimizeTimer = 0.0f;
	private Vector3 toolbarRectPos;
	private Vector3 shopRectPos;
	private Vector3 lowerMinMaxButtonPos;
    
	// Properties
	public bool CursorOnUI {
		get { return cursorOnUI; }
		set { cursorOnUI = value; }
	}
    
    // Initialization
    void Start () {
		buyingMachineGunNest = false;
		screenRes = Screen.currentResolution;
		cursorOnUI = false;
        deleteState = false;
		minimized = false;
		minmaxPress = false;
		shopRectTransf = shop.GetComponent<RectTransform>();
		toolbarRectTransf = toolbar.GetComponent<RectTransform>();
		StartCoroutine(LateStart(0.005f));
	}
	IEnumerator LateStart (float waitTime) {
		yield return new WaitForSeconds(waitTime);
		shopRectPos = shopRectTransf.position;
		toolbarRectPos = toolbarRectTransf.position;
		lowerMinMaxButtonPos = lowerMinMaxButton.transform.position;
	}

	// Update
	void Update () {
		if (!minimized && !cursorOnUI) {
			minimizeTimer += Time.deltaTime;
		}
		else {
			minimizeTimer = 0;
		}
	}

	// Late Update
	void LateUpdate () {
		if ((minimizeTimer >= timeToMinimize && !minimized) || (minmaxPress && !minimized)) {
			Minimize();
		}
		else if (minimized && minmaxPress) {
			Maximize();
		}
	}
	

	// -- Button Press Functions --------

	public void MachineGunNestPurchase () { // Attempt to purchase (GameGrid will handle the remaining checks)
		if (true/*Check if the player has enough money*/) {
			buyingMachineGunNest = true;
		}
	}

	public void MinMaxPress () {
		minmaxPress = true;
	}

    public void SetDeleteState()
    {
        if(deleteState)
        {
            deleteState = false;
        }
        else if(deleteState == false)
        {
            deleteState = true;
        }
        Debug.Log(deleteState);
    }


	// -- Minimize and Maximize Functions --------

	// Minimize function responsible for moving portion of game UI off the screen 
	void Minimize () {
		Vector3 curShopPos = shop.GetComponent<RectTransform>().position;
		Vector3 curToolbarPos = toolbar.GetComponent<RectTransform>().position;
		Vector3[] v1 = new Vector3[4]; //have to make array to get corners and calculate height in world space from RectTransf
		Vector3[] v2 = new Vector3[4];
		minmaxPress = true;
		shopRectTransf.GetWorldCorners(v1);
		shopRectTransf.GetWorldCorners(v2);
		float height1 = CalculateRectHeight(v1[0], v1[1]);
		float height2 = CalculateRectHeight(v2[0], v2[1]);
		if (curShopPos.y > shopRectPos.y - height1 - padding + 0.001f) {
			Vector3 smoothedPositionShop = Vector3.Lerp(curShopPos, shopRectPos - new Vector3(0, height1 + padding, 0), transitionSpeed);
			Vector3 smoothedPositionToolbar = Vector3.Lerp(curToolbarPos, toolbarRectPos - new Vector3(0, height2 + padding, 0), transitionSpeed);
			Vector3 smoothedPositionMinMaxButton = Vector3.Lerp(lowerMinMaxButton.transform.position, lowerMinMaxButtonPos - new Vector3(0, height1 + padding, 0), transitionSpeed);
			shop.transform.position = smoothedPositionShop;
			toolbar.transform.position = smoothedPositionToolbar;
			lowerMinMaxButton.transform.position = smoothedPositionMinMaxButton;
		} else {
			shopRectPos = curShopPos;
			toolbarRectPos = curToolbarPos;
			lowerMinMaxButtonPos = lowerMinMaxButton.transform.position;
			minimized = true;
			minmaxPress = false;
			minimizeTimer = 0;
		}
	}

	// Maximize function responsible for moving UI content back on screen
	void Maximize () {
		Vector3 curShopPos = shop.GetComponent<RectTransform>().position;
		Vector3 curToolbarPos = toolbar.GetComponent<RectTransform>().position;
		Vector3[] v1 = new Vector3[4]; //have to make array to get corners and calculate height in world space from RectTransf
		Vector3[] v2 = new Vector3[4];
		shopRectTransf.GetWorldCorners(v1);
		shopRectTransf.GetWorldCorners(v2);
		float height1 = CalculateRectHeight(v1[0], v1[1]);
		float height2 = CalculateRectHeight(v2[0], v2[1]);
		if (curShopPos.y < shopRectPos.y + height1 - padding - 0.001f) {
			Vector3 smoothedPositionShop = Vector3.Lerp(curShopPos, shopRectPos + new Vector3(0, height1 + padding, 0), transitionSpeed);
			Vector3 smoothedPositionToolbar = Vector3.Lerp(curToolbarPos, toolbarRectPos + new Vector3(0, height2 + padding, 0), transitionSpeed);
			Vector3 smoothedPositionMinMaxButton = Vector3.Lerp(lowerMinMaxButton.transform.position, lowerMinMaxButtonPos + new Vector3(0, height1 + padding, 0), transitionSpeed);
			shop.transform.position = smoothedPositionShop;
			toolbar.transform.position = smoothedPositionToolbar;
			lowerMinMaxButton.transform.position = smoothedPositionMinMaxButton;
		} else {
			shopRectPos = curShopPos;
			toolbarRectPos = curToolbarPos;
			lowerMinMaxButtonPos = lowerMinMaxButton.transform.position;
			minimized = false;
			minmaxPress = false;
			minimizeTimer = 0;
		}
	}

	float CalculateRectHeight (Vector3 vec1, Vector3 vec2) {
		return Mathf.Abs(vec2.y - vec1.y);
	}
}
