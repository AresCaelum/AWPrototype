using UnityEngine;
using System.Collections;

public class Player : MovableEntity
{
    static public Player instance;
    [SerializeField]
    Vector2 moveSpeed = Vector2.zero;
    [SerializeField]
    GameObject deathScene;
    [SerializeField]
    WeaponPowerUp baseBullet;
    [SerializeField]
    WeaponPowerUp weaponPowerUp;
    [SerializeField]
    Transform firePoint;

    Vector2 velocity = Vector2.zero;
    bool shooting = false;
    // Use this for initialization
    protected override void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (GameManager.paused || !GameManager.started_game)
            return;

        HandleInput();
        HandleUIUpdate();

        base.Update();
    }

    void HandleUIUpdate()
    {
        if (weaponPowerUp == null)
        {
            GameHud.UpdateWeaponIcon(baseBullet.getIcon());
            GameHud.HandleUIUpdateWeapon(baseBullet.getRatio());
        }
        else
        {
            GameHud.UpdateWeaponIcon(weaponPowerUp.getIcon());
            GameHud.HandleUIUpdateWeapon(weaponPowerUp.getRatio());
        }
    }

    protected override void UpdateAnimation()
    {
        myAnimator.SetFloat("InputH", velocity.x);
        myAnimator.SetBool("Walk", velocity.x != 0);
        myAnimator.SetBool("Fire", shooting);
        base.UpdateAnimation();
    }

    protected virtual void HandleInput()
    {
        myBody.angularVelocity = 0.0f;
        velocity = Vector2.zero;
        shooting = false;

        if (Input.touchCount > 0)
        {
            Vector2 touchPoint = Input.GetTouch(0).position;
            if (touchPoint.y < Screen.height * 0.75)
            {
                if (Input.touchCount > 1)
                {
                    shooting = true;
                }
                else
                {
                    if (touchPoint.x < Screen.width * 0.5f)
                        velocity.x = -1;
                    else
                        velocity.x = 1;
                }
            }
        }
        else
        {
            velocity.x = Input.GetAxisRaw("Horizontal");
            shooting = Input.GetKeyDown(KeyCode.Space);
        }

        myBody.MovePosition(new Vector2(transform.position.x + velocity.x * moveSpeed.x * Time.deltaTime, transform.position.y + velocity.y * moveSpeed.y * Time.deltaTime));
    }

    // Gets called by the animator
    public void Fire()
    {
        if (weaponPowerUp == null)
        {
            if (baseBullet.canFire())
                baseBullet.Fire(firePoint.position);
        }
        else
        {
            if (weaponPowerUp.canFire())
                weaponPowerUp.Fire(firePoint.position);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyProjectile")
        {
            LivesManager.instance.LostLife();
            Instantiate(deathScene, Vector3.zero, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void AddWeaponPowerUp(PowerUpObject powerUp)
    {
        if (weaponPowerUp)
        {
            // Add Remove old powerup
            weaponPowerUp.DestroySelf();
        }
        weaponPowerUp = (WeaponPowerUp)powerUp;
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void RemovePowerUp()
    {
        weaponPowerUp = null;
    }
}
