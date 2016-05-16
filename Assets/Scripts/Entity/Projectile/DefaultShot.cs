using UnityEngine;
using System.Collections;

public class DefaultShot : Projectile {
	static public int numShot = 0;

	[SerializeField] float moveSpeed = 5.0f;
	// Use this for initialization
	protected override void Start () {
		if (numShot > 0) {
			Debug.Log (numShot);
			Destroy (this.gameObject);
			return;
		}
		numShot++;
		base.Start ();
	}

	// Update is called once per frame
	protected override void Update () {
		myBody.MovePosition (new Vector2 (transform.position.x, transform.position.y + moveSpeed * Time.deltaTime));
		base.Update ();
	}

	protected override void UpdateAnimation()
	{
		base.UpdateAnimation ();
	}

	void HandleDeath()
	{
		numShot--;
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag != "Player") {
			HandleDeath ();
		}
	}
}
