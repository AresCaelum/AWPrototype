using UnityEngine;
using System.Collections;

public class PowerUpObject : StaticEntity {
	public GameObject ProjectileObject;
	[SerializeField] protected float lifeTime;
	protected float lifeLeft = 0;
	protected bool firable = true;

	// Use this for initialization
	override protected void Start () {
		lifeLeft = lifeTime;
		base.Start ();
	}
	
	// Update is called once per frame
	override protected void Update () {
		lifeLeft -= Time.deltaTime;
		if (lifeLeft <= 0.0f) {
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

	virtual public void Fire(Vector3 _position)
	{
		Fired ();
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
