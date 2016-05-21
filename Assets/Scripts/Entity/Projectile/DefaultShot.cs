using UnityEngine;
using System.Collections;

public class DefaultShot : Projectile {
	static DefaultShot instance = null;
	// Use this for initialization
	protected override void Start () {
		if(instance != null){
			Destroy (this.gameObject);
			return;
		}
		instance = this;
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

	void HandleDeath()
	{
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Enemy" || col.tag == "Platform") {
			HandleDeath ();
		}
	}

	void OnDestroy()
	{
		if(instance == this)
			instance = null;
	}
}
