using UnityEngine;
using System.Collections;

public class PowerUpObject : StaticEntity {
	public GameObject ProjectileObject;
	[SerializeField] float lifeTime;
	protected bool firable = true;

	// Use this for initialization
	override protected void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	override protected void Update () {
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0.0f) {
			if(Player.instance != null)
				Player.instance.RemovePowerUp ();
			Destroy (this.gameObject);
		}

		base.Update ();
	}

	public bool canFire()
	{
		return firable;
	}

	virtual public void Fired()
	{
		firable = false;
	}

	public void DestroySelf()
	{
		Destroy (this.gameObject);
	}
}
