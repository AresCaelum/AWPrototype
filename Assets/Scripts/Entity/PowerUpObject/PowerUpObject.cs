using UnityEngine;
using System.Collections;

public class PowerUpObject : StaticEntity {
	[SerializeField] protected float lifeTime;
	[SerializeField] protected Sprite UIicon;
	[SerializeField] bool dies = true;
	protected float lifeLeft = 0;

	// Use this for initialization
	override protected void Start () {
		lifeLeft = lifeTime;
		base.Start ();
	}
	
	// Update is called once per frame
	override protected void Update () {
		if (dies) {
			lifeLeft -= Time.deltaTime;
			if (lifeLeft <= 0.0f) {
				if (Player.instance != null)
					Player.instance.RemovePowerUp ();
				Destroy (this.gameObject);
			}
		}
		base.Update ();
	}

	virtual public float getRatio()
	{
		return 1.0f - (lifeLeft / lifeTime);
	}

	public Sprite getIcon()
	{
		return UIicon;
	}

	public void DestroySelf()
	{
		Destroy (this.gameObject);
	}
}
