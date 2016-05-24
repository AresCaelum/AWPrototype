using UnityEngine;
using System.Collections;

public class StaticPlatform : StaticEntity {
	[SerializeField] GameObject PowerUpToSpawn;
	[SerializeField] float lifeTimeOfSpawn = 15.0f;
	// Use this for initialization
	override protected void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	override protected void Update () {
		base.Update ();
	}

	void HandleDeath()
	{
		if (PowerUpToSpawn != null) {
			GameObject temp = Instantiate (PowerUpToSpawn, transform.position, PowerUpToSpawn.transform.rotation) as GameObject;
			Destroy (temp, lifeTimeOfSpawn);
		}	
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Projectile") {
			HandleDeath ();
		}
	}
}
