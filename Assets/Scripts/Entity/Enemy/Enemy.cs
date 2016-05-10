using UnityEngine;
using System.Collections;

public class Enemy : MovableEntity {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		HandleAI ();

		base.Update ();
	}

	protected override void UpdateAnimation ()
	{
		base.UpdateAnimation ();
	}

	protected virtual void HandleAI()
	{
		
	}
}
