using UnityEngine;
using System.Collections;

public class PowerUpObject : StaticEntity {
	public GameObject ProjectileObject;
	[SerializeField] float lifeTime;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0.0f) {
			if(Player.instance != null)
				Player.instance.RemovePowerUp ();
			Destroy (this.gameObject);
		}

	}

	public void DestroySelf()
	{
		Destroy (this.gameObject);
	}
}
