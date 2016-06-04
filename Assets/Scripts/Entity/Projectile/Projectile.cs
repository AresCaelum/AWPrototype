using UnityEngine;
using System.Collections;

public class Projectile : MovableEntity{
	[SerializeField]
	protected Vector2 moveSpeed;
	[SerializeField]
	protected int nDamage = 1;

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

	public void ScaleSpeed(float _xAmount, float _yAmount)
	{
		moveSpeed.x = moveSpeed.x * _xAmount;
		moveSpeed.y = moveSpeed.y * _yAmount;
	}

	protected override void UpdateAnimation()
	{
		base.UpdateAnimation ();
	}

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Enemy" || col.tag == "Platform") {
			col.gameObject.SendMessage ("TakeDamage", nDamage);
			Destroy (this.gameObject);
		} else if (col.tag == "Boundry") {
			Destroy (this.gameObject);
		}
	}
}
