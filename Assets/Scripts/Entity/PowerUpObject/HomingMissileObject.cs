using UnityEngine;
using System.Collections;

public class HomingMissileObject : PowerUpObject {
	[SerializeField]float fireRate;
	float fireTimer = 0.0f;

	// Use this for initialization
	override protected void Start () {
	}

	// Update is called once per frame
	override protected void Update () {
		if (fireTimer > 0.0f) {
			fireTimer -= Time.deltaTime;
			if (fireTimer <= 0.0f) {
				fireTimer = 0.0f;	
				firable = true;
			}
		}

		base.Update ();
	}

	override public void Fired()
	{
		fireTimer = fireRate;
		base.Fired ();
	}

	public void DestroySelf()
	{
		Destroy (this.gameObject);
	}
}
