using UnityEngine;
using System.Collections;

public class TripleShotPowerUp: WeaponPowerUp {

	// Use this for initialization
	override protected void Start () {
		base.Start ();
	}

	// Update is called once per frame
	override protected void Update () {

		base.Update ();
	}

	override public void Fire(Vector3 _position)
	{
		Instantiate (ProjectileObject, _position, Quaternion.identity);
		Instantiate (ProjectileObject, _position, Quaternion.Euler(new Vector3(0,0,45.0f)));
		Instantiate (ProjectileObject, _position, Quaternion.Euler(new Vector3(0,0,-45.0f)));
		Fired ();
	}

}
