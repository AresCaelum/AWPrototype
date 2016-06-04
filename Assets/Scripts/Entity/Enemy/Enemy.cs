using UnityEngine;
using System.Collections;

public class Enemy : MovableEntity {

	[SerializeField] protected GameObject powerUpOnDeath;
	[SerializeField] protected int nDamage = 0;

	// Use this for initialization
	protected override void Start () {
		GameManager.enemy_count++;
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

	protected override IEnumerator HandleDeathAnimation()
	{
		if (powerUpOnDeath != null) {
			GameObject temp = Instantiate (powerUpOnDeath, transform.position, powerUpOnDeath.transform.rotation) as GameObject;
			Destroy (temp, 15.0f);
		}
		GameManager.enemy_count--;
		Destroy (this.gameObject);
		yield return new WaitForSeconds(0.0f);
	}

	virtual protected void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") {
			col.gameObject.SendMessage ("TakeDamage", nDamage);
		}
	}
}
