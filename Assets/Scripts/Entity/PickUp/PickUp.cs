using UnityEngine;
using System.Collections;

public class PickUp : MovableEntity {
	[SerializeField]
	public System.Type typeToAdd;
	// Use this for initialization
	protected override void Start () {
	}
	
	// Update is called once per frame
	protected override void Update () {
	
	}

	protected override void UpdateAnimation()
	{
		base.UpdateAnimation ();
	}
}
