using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;

public class HUD : MonoBehaviour {

	public GUISkin statusSkin, orderSkin, selectBoxSkin;

	private const int orderBarWidth = 150, statusBarHeight = 40;
	private const int selectNameHeight = 20;

	//reference the player
	private Player player;

	// Use this for initialization
	void Start () {
		player = transform.root.GetComponent< Player > ();

		ResourceManagement.StoreSelectBoxItems (selectBoxSkin); 
	}

	// only show GUI if the player is a human
	void OnGUI () {
		if (player && player.human) {
			DisplayOrderBar ();
			DisplayStatusBar ();
		}
	}

	private void DisplayOrderBar () {
		GUI.skin = orderSkin;
		//define the rectangle area to display the skin
		GUI.BeginGroup (new Rect (Screen.width - orderBarWidth, statusBarHeight, orderBarWidth, Screen.height - statusBarHeight));
		GUI.Box (new Rect (0, 0, orderBarWidth, Screen.height - statusBarHeight), ""); //0 , 0 is relative to the group not the top left of the screen

		//show the name of the object in the orderbar
		string selectionName = "";
		if (player.SelectedObject) {
			selectionName = player.SelectedObject.objectName;
		}
		if (!selectionName.Equals ("")) { // if the selected objects name is the the same relabel
			GUI.Label (new Rect (0, 10, orderBarWidth, selectNameHeight), selectionName);
		}

		GUI.EndGroup (); // REMEMBER to end the group or an error will occur
	}

	private void DisplayStatusBar() {
		GUI.skin = statusSkin;
		GUI.BeginGroup(new Rect (0,0, Screen.width, statusBarHeight));
		GUI.Box(new Rect(0,0,Screen.width, statusBarHeight), "" );
		GUI.EndGroup();
	}

	// Used to find the HUD script in our player
	public bool MouseInBounds() {
		//Screen coordinates start at the lower-left corner of the screen not top left
		Vector3 mousePos = Input.mousePosition;
		bool insideWidth = mousePos.x >= 0 && mousePos.x <= Screen.width - orderBarWidth;
		bool insideHeight = mousePos.y >= 0 && mousePos.y <= Screen.height - statusBarHeight;
		return insideWidth && insideHeight; //checks to see if the mouse is within the screen excluding the HUD
	}

	// used in the Left and Right click mouse action
	public Rect GetPlayingArea() {
		return new Rect (0, statusBarHeight, Screen.width - orderBarWidth, Screen.height - statusBarHeight);
	}
}
