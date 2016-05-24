using UnityEngine;
using System.Collections;

public class DefenseObject : PowerUpObject {

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
}
