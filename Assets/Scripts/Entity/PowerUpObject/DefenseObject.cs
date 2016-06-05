using UnityEngine;
using System.Collections;

public class DefenseObject : PowerUpObject {
	[SerializeField]
	protected int nDamage = 1000;

	override protected void Start () {
		if (Player.instance != null) {
			transform.parent = Player.instance.gameObject.transform;
			transform.localPosition = new Vector3 (0, 0, -1);
		} else {
			Destroy (this.gameObject);
			return;
		}
		GameHud.ActivateDefenseUI ();
		base.Start ();
	}

	// Update is called once per frame
	override protected void Update () {
		GameHud.UpdateDefenseIcon (UIicon);
		GameHud.HandleUIUpdateArmor (getRatio ());
		base.Update ();
	}

	void OnDestroy()
	{
		GameHud.DeactivateDefenseUI ();
	}

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Enemy") {
			col.gameObject.SendMessage ("TakeDamage", nDamage);
		}
	}
}
