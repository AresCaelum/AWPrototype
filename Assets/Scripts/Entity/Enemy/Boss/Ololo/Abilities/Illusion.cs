using UnityEngine;
using System.Collections;

public class Illusion : Enemy
{
    float Alpha = 1.0f;
    float angle = -90;
    float speed = 500;
    float startHeight;
    float MoveSpeed = 5.0f;
    float toDegrees = Mathf.PI / 180;
    float maxUpAndDown = 0.1f;
    float FireTimer;
    float DefaultFireTimer = 1.0f;

    bool exploded = false;
    [SerializeField]
    GameObject Fireball;
    [SerializeField]
    GameObject Explosion;
    SpriteRenderer myEye;
    Ololo owner;

    [SerializeField]
    AudioClip ExplodeSFX;
    // Use this for initialization
    void Start()
    {
        FireTimer = DefaultFireTimer;
        startHeight = transform.position.y;
        myEye = GetComponent<SpriteRenderer>();

        if (owner.IsEnraged())
        {
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.25f, 0.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!owner.IsEnraged())
        {
            //MOVE UP AND DOWN
            angle += speed * Time.deltaTime;
            if (angle > 270)
                angle -= 360;
            float Yvalue = startHeight + maxUpAndDown * (1 + Mathf.Sin(angle * toDegrees));
            transform.localPosition = new Vector2(transform.localPosition.x, Yvalue);
        }
        

        if (GameManager.started_game)
            FireTimer -= Time.deltaTime;
        if (FireTimer <= 0)
        {
            if (!owner.IsEnraged())
            {
                GameObject temp = null;
                if (Fireball != null)
                    temp = Instantiate(Fireball, transform.position, Fireball.transform.rotation) as GameObject;

                if (temp != null)
                    Destroy(temp, 2.0f);
                FireTimer = DefaultFireTimer;
            }
            else
            {
                GameObject temp = null;
                float a = 315;
                for (int i = 0; i < 2; i++)
                {
                    temp = Instantiate(Fireball, transform.position, Fireball.transform.rotation) as GameObject;
                    temp.transform.eulerAngles = new Vector3(0, 0, a);
					temp.GetComponent<FireBall>().ScaleSpeed(1, 0.5f);
                    a += 90;
                    Destroy(temp, 3.0f);
                }
                FireTimer = DefaultFireTimer+1.0f;
            }

        }
    }

    public void SetOwner(Ololo own)
    {
        owner = own;
    }

    public void Kill()
    {
        if (Explosion && !exploded)
        {
            exploded = true;
            Instantiate(Explosion, transform.position, Explosion.transform.rotation);
            AudioManager.PlaySFX(ExplodeSFX);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Projectile")
        {
            Kill();
            owner.RemoveIllusion(gameObject);
            Destroy(gameObject);
        }
    }
}
