using UnityEngine; 
using System.Collections;

public class Player : MovableEntity {
	[SerializeField]Vector2 moveSpeed = Vector2.zero;
	[SerializeField]GameObject deathScene;
	[SerializeField]GameObject baseBullet;
	[SerializeField]Transform firePoint;
	GameObject WeaponPowerUp;

	Vector2 velocity = Vector2.zero;
	bool shooting = false;
	bool paused = false;
	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (GameManager.paused || !GameManager.started_game)
			return;
		
		HandleInput ();


		base.Update ();
	}

	protected override void UpdateAnimation()
	{
			myAnimator.SetFloat ("InputH", velocity.x);
			myAnimator.SetBool ("Walk", velocity.x != 0);
			myAnimator.SetBool ("Fire", shooting);
			base.UpdateAnimation ();
	}

	protected virtual void HandleInput()
	{
		myBody.angularVelocity = 0.0f;

		if (Input.GetMouseButton (0)) {
			if (Input.mousePosition.y < Screen.height * 0.25) {
				velocity = Vector2.zero;
				shooting = true;
			} else {
				if (Input.mousePosition.x < Screen.width * 0.5f)
					velocity.x = -1;
				else
					velocity.x = 1;

				shooting = false;
			}
		} else {
			velocity = Vector2.zero;
			shooting = false;
		}
			
		myBody.MovePosition(new Vector2 (transform.position.x + velocity.x * moveSpeed.x * Time.deltaTime, transform.position.y + velocity.y * moveSpeed.y* Time.deltaTime));
	}

	// Gets called by the animator
	public void Fire()
	{
		if (WeaponPowerUp == null) {
			Instantiate (baseBullet, firePoint.position, Quaternion.identity);
		} else {
			Instantiate (WeaponPowerUp, firePoint.position, Quaternion.identity);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy") {
			LivesManager.instance.LostLife ();
			Instantiate (deathScene, Vector3.zero, Quaternion.identity);
			Destroy (this.gameObject);
		}
	}

	public void AddPowerUp(GameObject powerUp)
	{
		if (WeaponPowerUp) {
			// Add Remove old powerup
			WeaponPowerUp.SendMessage("DestroySelf");
		}
		WeaponPowerUp = powerUp;
	}

	public void RemovePowerUp(GameObject powerUp)
	{
		WeaponPowerUp = null;
	}
}
