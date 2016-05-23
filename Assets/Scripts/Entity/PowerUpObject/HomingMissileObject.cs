using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HomingMissileObject : PowerUpObject {
	[SerializeField]float fireRate;
	[SerializeField]Image displayIcon;

	float fireTimer = 0.0f;

	// Use this for initialization
	override protected void Start () {
		base.Start ();
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
		displayIcon.material.SetFloat ("_Cutoff", lifeLeft / lifeTime);
		base.Update ();
	}

	override public void Fire(Vector3 _position)
	{
		Instantiate (ProjectileObject, _position, Quaternion.identity);
		base.Fire(_position);
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
