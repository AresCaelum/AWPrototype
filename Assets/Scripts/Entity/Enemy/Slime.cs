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
	protected override void Start () {
		GameManager.enemy_count++;
		base.Start ();
	}

	// Update is called once per frame
	protected override void Update () {
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

	protected override void UpdateAnimation ()
	{
		myAnimator.SetFloat ("Velocity", myBody.velocity.y);
		base.UpdateAnimation ();
	}

	protected virtual void HandleAI()
	{
		if (fLastYVelocity < 0 && myBody.velocity.y > 0 && bounceSound) {
			AudioManager.PlaySFX (bounceSound);
		}

		fLastYVelocity = myBody.velocity.y;

		if (Input.GetKeyDown (KeyCode.K)) {
			CreateChildren ();
		}
	}

	void OnDestroy()
	{
		if (GameManager.enemy_count > 0)
			GameManager.enemy_count--;
	}

	void CreateChildren()
	{
		if (mySize != SlimeSize.SMALL) {
			Vector3 offset = new Vector3 (0.1f, 0.0f, 0.1f);
			GameObject rightSlime = Instantiate (this.gameObject, transform.position + offset, Quaternion.identity) as GameObject;
			GameObject leftSlime = Instantiate (this.gameObject, transform.position - offset, Quaternion.identity) as GameObject;
			rightSlime.name = leftSlime.name = this.gameObject.name;

			Slime leftSlimeScript = leftSlime.GetComponent<Slime> ();
			Slime rightSlimeScript = rightSlime.GetComponent<Slime> ();

			if (leftSlimeScript) {
				leftSlimeScript.SetSize (mySize - 1);
				if (myBody.velocity.x < 0)
					leftSlimeScript.SetVelocity (new Vector2(myBody.velocity.x, myBody.velocity.y));
				else
					leftSlimeScript.SetVelocity (new Vector2(-myBody.velocity.x, myBody.velocity.y));
			}
			if (rightSlimeScript) {
				rightSlimeScript.SetSize (mySize - 1);
				if (myBody.velocity.x > 0)
					rightSlimeScript.SetVelocity (new Vector2(myBody.velocity.x, myBody.velocity.y));
				else
					rightSlimeScript.SetVelocity (new Vector2(-myBody.velocity.x, myBody.velocity.y));
			}
		}

		Destroy (this.gameObject);
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
