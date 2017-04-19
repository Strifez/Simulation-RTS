using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool human;
	public HUD hud;
	public WorldObject SelectedObject { get; set; } //needed for reference in the Camera Movement LeftClick

	// Use this for initialization
	void Start () {
		hud = GetComponentInChildren< HUD > ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
