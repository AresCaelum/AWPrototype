using UnityEngine; 
using System.Collections;

public class Player : MovableEntity {
	static public Player instance;
	[SerializeField]Vector2 moveSpeed = Vector2.zero;
	[SerializeField]GameObject deathScene;
	[SerializeField]GameObject baseBullet;
	[SerializeField]Transform firePoint;
	PowerUpObject WeaponPowerUp;

	Vector2 velocity = Vector2.zero;
	bool shooting = false;
	bool paused = false;
	// Use this for initialization
	protected override void Start () {
		if (instance != null) {
			Destroy (this.gameObject);
			return;
		}

		instance = this;
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
		velocity = Vector2.zero;
		shooting = false;

		if (Input.touchCount > 0) {
			Vector2 touchPoint= Input.GetTouch (0).position;
			if (touchPoint.y < Screen.height * 0.75) {
				if (Input.touchCount > 1) {
					shooting = true;
				} else {
					if (touchPoint.x < Screen.width * 0.5f)
						velocity.x = -1;
					else
						velocity.x = 1;
				}
			}
		} else {
			velocity.x = Input.GetAxisRaw("Horizontal");
			shooting = Input.GetKeyDown(KeyCode.Space);
		}
			
		myBody.MovePosition(new Vector2 (transform.position.x + velocity.x * moveSpeed.x * Time.deltaTime, transform.position.y + velocity.y * moveSpeed.y* Time.deltaTime));
	}

	// Gets called by the animator
	public void Fire()
	{
		if (WeaponPowerUp == null) {
			GameObject temp = Instantiate (baseBullet, firePoint.position, Quaternion.identity) as GameObject;
			Destroy (temp, 3.0f);
		} else {
			if (WeaponPowerUp.canFire ()) {
				Instantiate (WeaponPowerUp.ProjectileObject, firePoint.position, Quaternion.identity);
				WeaponPowerUp.Fired ();
			}
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

	public void AddPowerUp(PowerUpObject powerUp)
	{
		if (WeaponPowerUp) {
			// Add Remove old powerup
			powerUp.DestroySelf();
		}
		WeaponPowerUp = powerUp;
	}

	void OnDestroy()
	{
		if (instance == this) {
			instance = null;
		}
	}

	public void RemovePowerUp()
	{
		WeaponPowerUp = null;
	}
}
