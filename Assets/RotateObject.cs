using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
    public Vector3 Axis;
    public float Speed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Axis * Speed * Time.deltaTime);
    }
}
