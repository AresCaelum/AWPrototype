using UnityEngine;
using System.Collections;

enum OBOBO_STATES { MOVEMENT, SPAWN };
enum MOVE_DIRECTIONS { LEFT, RIGHT };
public class Obobo : MonoBehaviour
{
    [SerializeField]
    GameObject slime;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    GameObject deathExplosion;
    [SerializeField]
    GameObject ShieldPowerup;

    [SerializeField]
    RectTransform HP_BAR;

    OBOBO_STATES CurrentState = OBOBO_STATES.MOVEMENT;
    [SerializeField]
    MOVE_DIRECTIONS CurrentDirection = MOVE_DIRECTIONS.RIGHT;

    [SerializeField]
    int SlimesToSpawn = 1;

    [SerializeField]
    float HEALTH = 100.0f;
    float MoveSpeed = 5.0f;
    float maxUpAndDown = 1;
    [SerializeField]
    float speed = 500;
    [SerializeField]
    float XdistanceToTravel = 5;
    float angle = -90;
    float toDegrees = Mathf.PI / 180;
    float startHeight;
    float TimerToSpawn;
    float DefaultTimerToSpawn = 2.0f;

    bool Flashing = false;
    bool Dieing = false;
    bool SpawnSlimeOnLeftSide = true;

    Vector2 goalLeft;
    Vector2 goalRight;

    [SerializeField]
    AudioClip OboboTheme;
    [SerializeField]
    AudioClip ExplosionSFX;
    [SerializeField]
    AudioClip FinalExplosionSFX;
    [SerializeField]
    AudioClip HitSFX;
    [SerializeField]
    AudioClip SpawnSFX;
    // Use this for initialization
    void Start()
    {
        GameManager.enemy_count++;
        AudioManager.PlayBGM(OboboTheme, false);

        TimerToSpawn = DefaultTimerToSpawn;
        startHeight = transform.localPosition.y;
        goalLeft = new Vector2(transform.position.x - XdistanceToTravel, 0);
        goalRight = new Vector2(transform.position.x + XdistanceToTravel, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.started_game)
            TimerToSpawn -= Time.deltaTime;
        if (TimerToSpawn <= 0.0f)
        {
            CurrentState = OBOBO_STATES.SPAWN;
            TimerToSpawn = DefaultTimerToSpawn + TimerToSpawn;
        }
        switch (CurrentState)
        {
            case OBOBO_STATES.MOVEMENT:
                {
                    angle += speed * Time.deltaTime;
                    if (angle > 270)
                        angle -= 360;
                    float Yvalue = startHeight + maxUpAndDown * (1 + Mathf.Sin(angle * toDegrees));
                    transform.localPosition = new Vector2(transform.localPosition.x, Yvalue);

                    if (CurrentDirection == MOVE_DIRECTIONS.RIGHT)
                    {
                        goalRight = new Vector2(goalRight.x, transform.position.y);
                        transform.position = Vector2.MoveTowards(transform.position, goalRight, MoveSpeed * Time.smoothDeltaTime);
                        if (transform.position.x >= goalRight.x)
                            CurrentDirection = MOVE_DIRECTIONS.LEFT;
                    }
                    else if (CurrentDirection == MOVE_DIRECTIONS.LEFT)
                    {
                        goalLeft = new Vector2(goalLeft.x, transform.position.y);
                        transform.position = Vector2.MoveTowards(transform.position, goalLeft, MoveSpeed * Time.smoothDeltaTime);
                        if (transform.position.x <= goalLeft.x)
                            CurrentDirection = MOVE_DIRECTIONS.RIGHT;
                    }

                    break;
                }
            case OBOBO_STATES.SPAWN:
                {
                    if (!Dieing)
                    {
                        for (int i = 0; i < SlimesToSpawn; i++)
                        {
                            SpawnSlime(SpawnSlimeOnLeftSide);
                            SpawnSlimeOnLeftSide = !SpawnSlimeOnLeftSide;
                        }
                    }
                    break;
                }
            default:
                {

                    break;
                }
        }
        CurrentState = OBOBO_STATES.MOVEMENT;
    }

    IEnumerator GetHit()
    {
        Flashing = true;
        bool Switch = false;
        float elapsed = 0.0f;
        float duration = 1.0f;
        float red = 0.0f;
        SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
        AudioManager.PlaySFX(HitSFX);
        while (elapsed < duration)
        {
            if (!Switch)
                red += Time.deltaTime * 5.0f;
            else
                red -= Time.deltaTime * 5.0f;

            if (red >= 1.0f || red <= 0.0f)
                Switch = !Switch;

            myRenderer.color = new Color(red, 0, 0, 1);

            HP_BAR.localScale = new Vector3(HP_BAR.localScale.x - (1.5f * Time.deltaTime), HP_BAR.localScale.y, HP_BAR.localScale.z);
            if (HP_BAR.localScale.x <= 0)
                HP_BAR.localScale = new Vector3(0, HP_BAR.localScale.y, HP_BAR.localScale.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Flashing = false;
        myRenderer.color = Color.white;
    }

    void SpawnSlime(bool leftSide)
    {
        AudioManager.PlaySFX(SpawnSFX);
        Vector2 Pos = transform.position;
        if (leftSide)
            Pos.x -= 1;
        else
            Pos.x += 1;

        GameObject tempSlime = Instantiate(slime, Pos, slime.transform.rotation) as GameObject;
        Slime temp = null;
        if (tempSlime)
            temp = tempSlime.GetComponent<Slime>();
    }

    IEnumerator StageDeath()
    {
        Flashing = true;
        MoveSpeed *= 0.1f;
        speed *= 0.1f;
        float elapsed = 0.0f;
        float duration = 2.0f;
        float spawn = 0.1f;
        float color = 1.0f;
        Vector2 pos;
        SpriteRenderer myRenderer = null;
        SpriteRenderer myPupilRenderer = null;
        if (GetComponent<SpriteRenderer>())
            myRenderer = GetComponent<SpriteRenderer>();

        if (transform.GetChild(0) == null)
            Debug.Log("Obobo.cs: Line 181 - GetChild(0) is null");
        else
            myPupilRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        while (elapsed < duration)
        {
            spawn -= Time.deltaTime;
            if (spawn <= 0)
            {
                pos.x = transform.position.x + (Random.value * 2 - 1);
                pos.y = transform.position.y + (Random.value * 2 - 1);

                if (explosion != null)
                {
                    AudioManager.PlaySFX(ExplosionSFX);
                    Instantiate(explosion, pos, explosion.transform.rotation);
                }

                spawn = 0.1f;
            }

            color -= Time.deltaTime * 0.5f;
            if (myRenderer)
                myRenderer.color = new Color(color, color, color, color);
            if (myPupilRenderer)
                myPupilRenderer.color = new Color(color, color, color, color);
            elapsed += Time.deltaTime;
            yield return null;
        }
        if (deathExplosion != null)
        {
            AudioManager.PlaySFX(FinalExplosionSFX);
            pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            Instantiate(deathExplosion, transform.position, deathExplosion.transform.rotation);
        }
        if(ShieldPowerup)
            Instantiate(ShieldPowerup, transform.position, ShieldPowerup.transform.rotation);

    }

    IEnumerator KillBoss()
    {
        yield return new WaitForSeconds(3.0f);
        GameManager.enemy_count--;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Projectile" && !Flashing)
        {
            StartCoroutine(GetHit());
            HEALTH -= 10;
            if (HEALTH <= 0.0f)
            {
                StartCoroutine(StageDeath());
                StartCoroutine(KillBoss());
            }

        }
    }
}
