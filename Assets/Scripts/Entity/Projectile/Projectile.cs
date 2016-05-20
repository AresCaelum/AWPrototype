using UnityEngine;
using System.Collections;

public class Projectile : MovableEntity{
	[SerializeField]
	protected Vector2 moveSpeed;
	// Use this for initialization
	protected override void Start () {
	
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {


		Vector2 offsetPosition = (new Vector2(transform.up.x * moveSpeed.y, transform.up.y * moveSpeed.y) + new Vector2(transform.right.x * moveSpeed.x, transform.right.y * moveSpeed.x)) * Time.deltaTime;
		myBody.MovePosition (new Vector2 (transform.position.x , transform.position.y) + offsetPosition); 
		base.Update ();
	}

	protected override void UpdateAnimation()
	{
		base.UpdateAnimation ();
	}
}
