using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;

public class WorldObject : MonoBehaviour {

	public string objectName;
	public Texture2D buildImage;
	public int cost, sellValue, hitPoints, maxHitPoints;

	protected Player player;
	protected string[] actions = {};
	protected bool currentlySelected = false;

	protected Bounds selectionBounds;
	protected Rect playingArea = new Rect (0.0f, 0.0f, 0.0f, 0.0f);

	//making sure the subclass can have its own implementation we use vitual
	protected virtual void Awake() {
		//always called before start
		selectionBounds = ResourceManagement.InvalidBounds;
		CalculateBounds();
	}
	// Use this for initialization
	protected virtual void Start () {
		player = transform.root.GetComponentInChildren< Player >();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

	protected virtual void OnGUI() {
		if (currentlySelected) {
			DrawSelection ();
		}
	}

	//making an object selectable
	public void SetSelect (bool selected, Rect playingArea) {
		currentlySelected = selected;
		if (selected) {
			this.playingArea = playingArea;
			player.hud.GetPlayingArea ();
		}


	}

	public void SetPlayingArea(Rect playingArea) {
		this.playingArea = playingArea;
	}

	public string[] GetActions() {
		return actions;
	}

	public virtual void PerformAction (string actionToPerform){
		//it is up to the children with specific action to determine what to do with each of those actions
	}

	// used to handle the mouse clicks for any world object
	public virtual void MouseClick (GameObject hitObject, Vector3 hitPoint, Player controller) {
		//only handle input if currently selected
		if (currentlySelected && hitObject && hitObject.name != "Ground") {
			WorldObject worldObject = hitObject.transform.root.GetComponent< WorldObject > ();
			//Debug.Log (hitObject.name);

			//clicked on another selectable object
			if (worldObject) {
				ChangeSelection (worldObject, controller);
			}
		}
	}

	// ChangeSelection used in the MouseClick
	private void ChangeSelection(WorldObject worldObject, Player controller){
		SetSelect (false, playingArea);
		if (controller.SelectedObject) {
			controller.SelectedObject.SetSelect (false, playingArea); //deselect the previous selected object
			controller.SelectedObject = worldObject; //make the new selected object become selected
			worldObject.SetSelect (true, controller.hud.GetPlayingArea()); 
		}
	}

	// Draw the selection box when the character is selected
	private void DrawSelection() {
		GUI.skin = ResourceManagement.SelectBoxSkin;
		Rect selectBox = WorkManager.CalculateSelectionBox (selectionBounds, playingArea);

		//Draw the box around the selected object within the playing area
		GUI.BeginGroup(playingArea);
		DrawSelectionBox (selectBox);
		GUI.EndGroup ();
	}

	// draws the box with no text inside
	protected virtual void DrawSelectionBox( Rect selectBox) {
		GUI.Box (selectBox, "");
	}

	// This method ensures that our object's rectangle is being calculated correctly for every world object
	public void CalculateBounds() {
		selectionBounds = new Bounds (transform.position, Vector3.zero);
		foreach (Renderer r in GetComponentsInChildren< Renderer > ()) {
			selectionBounds.Encapsulate (r.bounds);
		}
	}

	public Bounds GetSelectionBounds() {
		return selectionBounds;
	}


}
