using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;

public class CameraMovement : MonoBehaviour {

	private Player player;

	// Use this for initialization
	void Start () {
		player = transform.root.GetComponent<Player> (); //tell unity to access the root of the object named Unit
	}

	// Update is called once per frame
	void Update () {
		if (player.human) {
			MoveCamera ();
			RotateCamera ();
			MouseActions ();
		}

	}

	private void MoveCamera(){
		float xPos = Input.mousePosition.x;
		float yPos = Input.mousePosition.y;
		Vector3 movement = new Vector3 (0, 0, 0);

		//Horizontal Camera Movement on X axis
		if (xPos >= 0 && xPos < ResourceManagement.sWidth) {
			movement.x -= ResourceManagement.sSpeed;
		} else if (xPos <= Screen.width && xPos > Screen.width - ResourceManagement.sWidth) {
			movement.x += ResourceManagement.sSpeed;
		}

		//Vertical Camera Movement on Y axis
		if (yPos >= 0 && yPos < ResourceManagement.sWidth) {
			movement.z -= ResourceManagement.sSpeed;
		} else if (yPos <= Screen.width && yPos > Screen.height - ResourceManagement.sWidth) {
			movement.z += ResourceManagement.sSpeed;
		}

		// removing the tilting of camera
		movement = Camera.main.transform.TransformDirection(movement);
		movement.y = 0;

		// vertical movement of the camera on the mouse wheel
		movement.y -= ResourceManagement.sSpeed * Input.GetAxis ("Mouse ScrollWheel");

		// fixed Camera Position and make the camera go where the mouse points to
		Vector3 origin = Camera.main.transform.position;
		Vector3 destination = origin;
		destination.x += movement.x;
		destination.y += movement.y;
		destination.z += movement.z;

		// apply the limits to the camera from the ResourceManagement Script
		if (destination.y > ResourceManagement.MaxCamHeight) {
			destination.y = ResourceManagement.MaxCamHeight;
		} else if (destination.y < ResourceManagement.MinCamHeight) {
			destination.y = ResourceManagement.MinCamHeight;
		}

		// if the mouse touches the edge Move the Camera
		if (destination != origin) {
			Camera.main.transform.position = Vector3.MoveTowards (origin, destination, Time.deltaTime * ResourceManagement.sSpeed);
		}
	}

	private void RotateCamera() {
		Vector3 origin = Camera.main.transform.eulerAngles; //rotation in euler
		Vector3 destination = origin;

		if(Input.GetMouseButton(2)) {
			destination.x -= Input.GetAxis ("Mouse Y") * ResourceManagement.rAmount;
			destination.y += Input.GetAxis ("Mouse X") * ResourceManagement.rAmount;
		}

		if (destination != origin) {
			Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManagement.rSpeed);
		}
	}


	// Mouse Activity, make things selectable and show it on the UI
	private void MouseActions() {
		if (Input.GetMouseButtonDown (0)) {
			LeftMouseClick ();
		} else if (Input.GetMouseButtonDown(1)) {
			RightMouseClick ();
		}
	}

	// raycasting to find the objects hitpoints
	private GameObject FindHitObject() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			return hit.collider.gameObject;
		}
		return null;
	}

	private Vector3 FindHitPoint() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			return hit.point;
		}
		return ResourceManagement.InvalidPosition;
	}

	// determine what the leftmouse button is going to do
	private void LeftMouseClick() {
		if (player.hud.MouseInBounds ()) {
			GameObject hitObject = FindHitObject ();
			Vector3 hitPoint = FindHitPoint ();

			if (hitObject && hitPoint != ResourceManagement.InvalidPosition) { // invalid position would be the sky for example, left mouse will ignore
				if (player.SelectedObject) {
					player.SelectedObject.MouseClick (hitObject, hitPoint, player); 
				} else if (hitObject.name != "Ground"){
					WorldObject worldObject = hitObject.transform.root.GetComponent< WorldObject > ();

					if (worldObject) { //if the object clicked on was not the ground and it is a world object, we can select it
						player.SelectedObject = worldObject;
						worldObject.SetSelect (true, playingArea:player.hud.GetPlayingArea());
					}
				}
			}
		}
	}

	//deselection using the right mouse button
	private void RightMouseClick() {
		if(player.hud.MouseInBounds() && !Input.GetKey(KeyCode.LeftAlt) && player.SelectedObject) {
			player.SelectedObject.SetSelect (false, playingArea:player.hud.GetPlayingArea());
			player.SelectedObject = null;
		}
	}

}
