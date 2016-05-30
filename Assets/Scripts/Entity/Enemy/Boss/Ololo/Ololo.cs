using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum OLOLO_STATES { SHOOTING, TELEPORTING, BOTH };

public class Ololo : MonoBehaviour
{
    [SerializeField]
    GameObject TeleportingEffect;
    [SerializeField]
    GameObject Fireball;
    [SerializeField]
    GameObject illusion;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    GameObject deathExplosion;
    [SerializeField]
    RectTransform HP_BAR;

    List<GameObject> IllusionsSpawned = new List<GameObject>();
    List<Vector2> IllusionPositions = new List<Vector2>();

    Color originalColor = Color.white;
    float HEALTH = 100.0f;
    [SerializeField]
    float TimeToTeleport = 1.0f;
    float FireCoolDown = 1.0f;
    float Radius = 2.5f;
    float angle = -90;
    float speed = 500;
    float startHeight;
    float MoveSpeed = 5.0f;
    float toDegrees = Mathf.PI / 180;
    float maxUpAndDown = 0.1f;
    float FireTimer;
    float DefaultFireTimer = 1.0f;

    int EnragedFireballAmount = 2;
    int NumberOfIllusions = 3;

    bool Teleporting = false;
    bool Flashing = false;
    bool Dieing = false;
    bool Enraged = false;

    [SerializeField]
    AudioClip OloloTheme;
    [SerializeField]
    AudioClip ExplosionSFX;
    [SerializeField]
    AudioClip FinalExplosionSFX;
    [SerializeField]
    AudioClip HitSFX;
    [SerializeField]
    // Use this for initialization
    void Start()
    {
        AudioManager.PlayBGM(OloloTheme, false);
        FireTimer = DefaultFireTimer;
        startHeight = transform.position.y;
        Radius = GetComponent<CircleCollider2D>().radius;
        GameManager.enemy_count++;
    }

    // Update is called once per frame
    void Update()
    {
        //TESTING DEATH
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    StartCoroutine(StageDeath());
        //    StartCoroutine(KillBoss());
        //}
        if (IllusionsAreGone() && !Teleporting && !Flashing && !Dieing)
            StartCoroutine(Teleport());
        if (!Teleporting && !Dieing && !Enraged)
        {
            angle += speed * Time.deltaTime;
            if (angle > 270)
                angle -= 360;
            float Yvalue = startHeight + maxUpAndDown * (1 + Mathf.Sin(angle * toDegrees));
            transform.localPosition = new Vector2(transform.localPosition.x, Yvalue);
        }

        if (GameManager.started_game && !Teleporting && !Flashing && !Dieing)
            FireTimer -= Time.deltaTime;
        if (FireTimer <= 0)
        {
            if (Enraged)
                ShootAllAround();
            else
                Shoot();
        }
    }

    bool IllusionsAreGone()
    {
        if (IllusionsSpawned.Count == 0)
            return true;

        return false;
    }

    public void RemoveIllusion(GameObject obj)
    {
        obj.GetComponent<Illusion>().Kill();
        IllusionsSpawned.Remove(obj);
    }

    public void CleanIllusions()
    {
        foreach (GameObject child in IllusionsSpawned)
        {
            child.GetComponent<Illusion>().Kill();
            Destroy(child);
        }
        IllusionsSpawned.Clear();
    }

    IEnumerator Teleport()
    {
        Teleporting = true;
        FireTimer = DefaultFireTimer;
        Quaternion OriginalRotation = transform.rotation;
        float elapsed = 0.0f;
        float RotationPerSecond = 10.0f;
        if (TeleportingEffect != null)
            Instantiate(TeleportingEffect, transform.position, TeleportingEffect.transform.rotation);

        StartCoroutine(GetPositions());
        while (elapsed < TimeToTeleport)
        {
            RotationPerSecond += Time.deltaTime * 50.0f;
            transform.Rotate(new Vector3(0, 0, RotationPerSecond));

            elapsed += Time.deltaTime;
            yield return null;
        }

        SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.color = originalColor;
        Flashing = false;

        StartCoroutine(SpawnIllusions());
    }

    IEnumerator GetPositions()
    {
        IllusionPositions.Clear();
        yield return new WaitUntil(() => IllusionPositions.Count == 0);

        StartCoroutine(CreatePositions());
    }

    IEnumerator CreatePositions()
    {
        while (IllusionPositions.Count < NumberOfIllusions)
        {
            float Yvalue;
            if (!Enraged)
                Yvalue = Random.Range(0.0f, 2500.0f) * 0.001f;
            else
                Yvalue = 1.25f;

            float Xvalue = Random.Range(-5.0f, 5.0f);
            Vector2 Pos = new Vector2(Xvalue, Yvalue);

            if (Vector2.Distance(Pos, transform.position) > Radius)
            {
                bool Redo = false;
                foreach (Vector2 child in IllusionPositions)
                {
                    if (Vector2.Distance(Pos, child) <= Radius)
                    {
                        Redo = true;
                        break;
                    }
                }
                if (Redo)
                    continue;
                else
                    IllusionPositions.Add(Pos);
            }
            yield return null;
        }
    }

    IEnumerator SpawnIllusions()
    {
        yield return new WaitUntil(() => true /*IllusionPositions.Count == NumberOfIllusions*/);
        for (int i = 0; i < IllusionPositions.Count; i++)
        {
            if (illusion == null)
            {
                Debug.Log("Illusion is null, please attach illusion gameobject.");
                break;
            }
            GameObject temp = Instantiate(illusion, IllusionPositions[i], illusion.transform.rotation) as GameObject;
            temp.name = "OloloIllusion";
            if (temp)
                temp.GetComponent<Illusion>().SetOwner(this);

            IllusionsSpawned.Add(temp);
        }

        float Yvalue;
        float Xvalue;
        while (true)
        {
            if (!Enraged)
                Yvalue = Random.Range(0.0f, 2.5f);
            else
                Yvalue = 1.25f;
            Xvalue = Random.Range(-5.0f, 5.0f);
            Vector2 Pos = new Vector2(Xvalue, Yvalue);

            bool Redo = false;
            foreach (Vector2 child in IllusionPositions)
            {
                if (Vector2.Distance(Pos, child) <= Radius)
                {
                    Redo = true;
                    break;
                }
            }
            if (Redo)
                continue;
            else
            {
                int randomIllusion = Random.Range(0, IllusionsSpawned.Count - 1);
                transform.position = IllusionsSpawned[randomIllusion].transform.position;
                IllusionsSpawned[randomIllusion].transform.position = new Vector2(Xvalue, Yvalue);
                transform.rotation = new Quaternion(0, 0, 0, 1);
                startHeight = transform.position.y;
                break;
            }
        }
        Teleporting = false;
    }

    IEnumerator GetHit(int num)
    {
        Flashing = true;
        CleanIllusions();

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

            HP_BAR.localScale = new Vector3(HP_BAR.localScale.x - ((1.5f * 0.5f) * num * Time.deltaTime), HP_BAR.localScale.y, HP_BAR.localScale.z);
            if (HP_BAR.localScale.x <= 0)
                HP_BAR.localScale = new Vector3(0, HP_BAR.localScale.y, HP_BAR.localScale.z);

            elapsed += Time.deltaTime;
            yield return null;
        }


        StartCoroutine(Teleport());
    }

    void Shoot()
    {
        GameObject temp = null;
        if (Fireball != null)
            temp = Instantiate(Fireball, transform.position, Fireball.transform.rotation) as GameObject;

        if (temp != null)
            Destroy(temp, 2.0f);
        FireTimer = DefaultFireTimer;
    }

    void ShootAllAround()
    {
        GameObject temp = null;
        float angle = 315;
        for (int i = 0; i < EnragedFireballAmount; i++)
        {
            temp = Instantiate(Fireball, transform.position, Fireball.transform.rotation) as GameObject;
            temp.transform.eulerAngles = new Vector3(0, 0, angle);
            temp.GetComponent<FireBall>().movespeed *= 0.5f;
            angle += 90;
            Destroy(temp, 3.0f);
        }
        FireTimer = DefaultFireTimer+1.0f;
    }

    IEnumerator StageDeath()
    {
        Dieing = true;
        CleanIllusions();
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
    }

    IEnumerator KillBoss()
    {
        yield return new WaitForSeconds(3.0f);
        GameManager.enemy_count--;
        Destroy(gameObject);
    }

    public bool IsEnraged()
    {
        return Enraged;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Projectile")
        {
            if (!Flashing)
            {
                HEALTH -= 5 * IllusionsSpawned.Count;
                StartCoroutine(GetHit(IllusionsSpawned.Count));

                if (HEALTH <= 0)
                {
                    StartCoroutine(StageDeath());
                    StartCoroutine(KillBoss());
                }
                if (HEALTH < 40)
                {
                    Enraged = true;
                    originalColor = new Color(0.5f, 0.25f, 0.25f);
                }
            }
        }
    }

}
