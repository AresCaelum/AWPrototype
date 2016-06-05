using UnityEngine;
using System.Collections;

public class WeaponPowerUp : PowerUpObject {
	[SerializeField]protected float fireRate;
	[SerializeField]protected GameObject ProjectileObject;
	[SerializeField]protected float projectileLifeTime = 5.0f;
	protected float fireTimer = 0.0f;
	protected bool firable = true;

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
		base.Update ();
	}

	virtual public void Fire(Vector3 _position)
	{
		GameObject temp = Instantiate (ProjectileObject, _position, Quaternion.identity) as GameObject;
		Destroy (temp, projectileLifeTime);
		Fired ();
	}

	virtual public void Fired()
	{
		fireTimer = fireRate;
		firable = false;
	}

	override public float getRatio()
	{
		return 1.0f - (fireTimer / fireRate);
	}

	virtual public  bool canFire()
	{
		return firable;
	}
}
