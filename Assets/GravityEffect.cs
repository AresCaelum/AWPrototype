using UnityEngine;
using System.Collections;

public class GravityEffect : MonoBehaviour {
	public float force = 10.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Enemy") {
			Rigidbody2D temp = col.gameObject.GetComponent<Rigidbody2D> ();
			if (temp != null) {
				Vector2 forceDirection =  transform.position - temp.transform.position;
				temp.AddForce (forceDirection * force * Time.deltaTime);
			}
		}
	}
}
