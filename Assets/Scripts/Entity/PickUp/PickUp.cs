using UnityEngine;
using System.Collections;

public class PickUp : MovableEntity {
	[SerializeField] GameObject powerUpObject;
	[SerializeField] AudioClip pickUpSound;
	[SerializeField] float Gravity = 2.5f;
	// Use this for initialization
	protected override void Start () {
		myBody.gravityScale = Gravity;
	}
	
	// Update is called once per frame
	protected override void Update () {
		myBody.MovePosition (new Vector2 (transform.position.x, transform.position.y - Gravity * Time.deltaTime));
		base.Update ();
	}

	protected override void UpdateAnimation()
	{
		base.UpdateAnimation ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			if (powerUpObject != null) {
				GameObject temp = Instantiate (powerUpObject, Vector3.zero, Quaternion.identity) as GameObject;
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
