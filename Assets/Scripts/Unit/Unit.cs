using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : WorldObject { //inherit from WorldObject and to override Unity Methods

	protected override void Awake(){
		base.Awake ();
	}
	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void OnGUI () {
		base.OnGUI();
	}
}
