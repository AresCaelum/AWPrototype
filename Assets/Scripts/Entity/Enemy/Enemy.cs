using UnityEngine;
using System.Collections;

public class Enemy : MovableEntity {

	[SerializeField] protected GameObject powerUpOnDeath;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (GameManager.paused)
			return;
		HandleAI ();

		base.Update ();
	}

	protected override void UpdateAnimation ()
	{
		base.UpdateAnimation ();
	}

	protected virtual void HandleAI()
	{
		
	}

	protected void HandleDeath()
	{
		if (powerUpOnDeath != null) {
			GameObject temp = Instantiate (powerUpOnDeath, transform.position, powerUpOnDeath.transform.rotation) as GameObject;
			Destroy (temp, 15.0f);
		}
		Destroy (this.gameObject);
	}
}
