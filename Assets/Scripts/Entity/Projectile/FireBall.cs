using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour
{
    public float movespeed = -5.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * movespeed * Time.deltaTime, Space.World);
    }
}
