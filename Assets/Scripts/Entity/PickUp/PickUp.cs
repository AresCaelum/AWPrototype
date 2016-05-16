using UnityEngine;
using System.Collections;

public class PickUp : MovableEntity {
	[SerializeField] GameObject powerUpObject;
	[SerializeField] AudioClip pickUpSound;
	// Use this for initialization
	protected override void Start () {
	}
	
	// Update is called once per frame
	protected override void Update () {
	
	}

	protected override void UpdateAnimation()
	{
		base.UpdateAnimation ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			if (powerUpObject != null) {
				GameObject temp = Instantiate (powerUpObject, new Vector3 (100, 100, 1), Quaternion.identity) as GameObject;
				PowerUpObject puObject = temp.GetComponent<PowerUpObject> ();
				if (puObject != null) {
					if (Player.instance != null) {
						Player.instance.AddPowerUp (puObject);
					}
				}
			}
			if (pickUpSound != null)
				AudioManager.PlaySFX (pickUpSound);
			Destroy (this.gameObject);
		}
	}
}
