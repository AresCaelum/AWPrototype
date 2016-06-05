using UnityEngine;
using System.Collections;

public class Slime : Enemy{
	public enum SlimeSize {SMALL, MEDIUM, BIG};

	[SerializeField]AudioClip bounceSound;
	[SerializeField]AudioClip deathSound;
	[SerializeField]SlimeSize mySize = SlimeSize.BIG;
	[SerializeField]Vector2 startingVelocity;


	float fLastYVelocity = 0.0f;
	bool initialized = false;

	// Use this for initialization
	override protected void Start () {
		
		base.Start ();
	}

	// Update is called once per frame
	override protected void Update () {
		if (!GameManager.started_game) {
			myBody.gravityScale = 0;
			return;
		} else if (!initialized) {
			myBody.gravityScale = 2;
			SetSize (mySize);
			SetVelocity (startingVelocity);
			initialized = true;
		}
			
		HandleAI ();

		base.Update ();
	}

	override protected void UpdateAnimation ()
	{
		myAnimator.SetFloat ("Velocity", myBody.velocity.y);
		base.UpdateAnimation ();
	}

	override protected void HandleAI()
	{
		if (fLastYVelocity < 0 && myBody.velocity.y > 0 && bounceSound) {
			AudioManager.PlaySFX (bounceSound);
		}

		fLastYVelocity = myBody.velocity.y;

		base.HandleAI ();
	}

	protected override IEnumerator HandleDeathAnimation()
	{
		if (mySize != SlimeSize.SMALL) {
			Color color = gameObject.GetComponent<SpriteRenderer> ().color;

			Vector3 offset = new Vector3 (0.1f, 0.0f, 0.1f);
			GameObject rightSlime = Instantiate (this.gameObject, transform.position + offset, Quaternion.identity) as GameObject;
			GameObject leftSlime = Instantiate (this.gameObject, transform.position - offset, Quaternion.identity) as GameObject;
			rightSlime.name = leftSlime.name = this.gameObject.name;

			Slime leftSlimeScript = null;
			Slime rightSlimeScript = null;

			if (leftSlime != null) {
				leftSlimeScript = leftSlime.GetComponent<Slime> ();
				SpriteRenderer lsRenderer = leftSlime.GetComponent<SpriteRenderer> ();
				if (lsRenderer != null)
					lsRenderer.color = color;
			}
			if (rightSlime != null) {
				rightSlimeScript = rightSlime.GetComponent<Slime> ();
				SpriteRenderer rsRenderer = rightSlime.GetComponent<SpriteRenderer> ();
				if (rsRenderer != null)
					rsRenderer.color = color;
			}

			if (leftSlimeScript != null) {
				leftSlimeScript.SetSize (mySize - 1);
				if (myBody.velocity.x < 0)
					leftSlimeScript.SetVelocity (new Vector2((myBody.velocity.x != 0.0f ? myBody.velocity.x : -2.0f), 6));
				else
					leftSlimeScript.SetVelocity (new Vector2((myBody.velocity.x != 0.0f ? -myBody.velocity.x : -2.0f), 6));

				leftSlimeScript.powerUpOnDeath = null;
			}
			if (rightSlimeScript != null) {
				rightSlimeScript.SetSize (mySize - 1);
				if (myBody.velocity.x > 0)
					rightSlimeScript.SetVelocity (new Vector2((myBody.velocity.x != 0.0f ? myBody.velocity.x : 2.0f), 6));
				else
					rightSlimeScript.SetVelocity (new Vector2((myBody.velocity.x != 0.0f ? -myBody.velocity.x : 2.0f), 6));
				rightSlimeScript.powerUpOnDeath = null;
			}
		}

		yield return base.HandleDeathAnimation ();
	}

	public void SetSize(SlimeSize newSize)
	{
		mySize = newSize;
		switch (mySize) {
			case SlimeSize.MEDIUM:
			{
				transform.localScale = new Vector3 (0.66f, 0.66f, 1);
				break;
			}
			case SlimeSize.SMALL:
			{
				transform.localScale = new Vector3 (0.33f, 0.33f, 1);
				break;
			}
		default:
			break;
		}
	}

	void SetVelocity(Vector2 vel)
	{
		startingVelocity = vel;
		if(GameManager.started_game)
			myBody.velocity = vel;
	}
}
