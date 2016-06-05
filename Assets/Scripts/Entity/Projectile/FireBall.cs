using UnityEngine;
using System.Collections;

public class FireBall : Projectile
{
	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void UpdateAnimation()
	{
		base.UpdateAnimation ();
	}
	protected override void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player") {
			col.gameObject.SendMessage ("TakeDamage", nDamage);
			Destroy (this.gameObject);
		}
	}
}
