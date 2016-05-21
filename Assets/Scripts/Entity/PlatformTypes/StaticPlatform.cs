using UnityEngine;
using System.Collections;

public class StaticPlatform : StaticEntity {
	[SerializeField] GameObject PowerUpToSpawn;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void HandleDeath()
	{
		if (PowerUpToSpawn != null) {
			GameObject temp = Instantiate (PowerUpToSpawn, transform.position, Quaternion.identity) as GameObject;
			Destroy (temp, 15.0f);
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
