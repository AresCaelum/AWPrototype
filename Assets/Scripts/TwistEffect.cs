using UnityEngine;
using System.Collections;

public class TwistEffect : MonoBehaviour {

	public float rotationRate = 45.0f;

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0.0f, rotationRate * Time.deltaTime));
	}
}
