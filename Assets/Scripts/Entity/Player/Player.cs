using UnityEngine; 
using System.Collections;

public class Player : MovableEntity {
	[SerializeField]
	Vector2 moveSpeed = Vector2.zero;
	Vector2 velocity = Vector2.zero;
	bool shooting = false;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		HandleInput ();
		base.Update ();
	}

	protected override void UpdateAnimation()
	{
		myAnimator.SetFloat ("InputH", velocity.x);
		myAnimator.SetBool ("Fire", shooting);
		base.UpdateAnimation ();
	}

	protected virtual void HandleInput()
	{
		myBody.angularVelocity = 0.0f;

		velocity.x = Input.GetAxis ("Horizontal");
		shooting = Input.GetButtonDown ("Fire");

		myBody.velocity = new Vector2 (velocity.x * moveSpeed.x * Time.deltaTime, velocity.y * moveSpeed.y * Time.deltaTime);
	}

	// Gets called by the animator
	public void Fire()
	{

	}
}
