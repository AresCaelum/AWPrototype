using UnityEngine;
using System.Collections;

public class HomingMissile : Projectile {
	Transform target;
	[SerializeField]
	float rotationSpeed = 135.0f;

	// Use this for initialization
	protected override void Start () {
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Enemy");
		if (temp != null) {
			target = temp[Random.Range (0, temp.Length)].transform;
		}
		base.Start ();
	}

	// Update is called once per frame
	protected override void Update () {
		if (target != null) {
			Vector3 direction = target.position - transform.position;
			float angle = Vector3.Dot(direction.normalized, transform.right.normalized);
			if (angle > 0)
				angle = -1;
			else if (angle < 0)
				angle = 1;
			Debug.Log (angle);
			myBody.MoveRotation (transform.rotation.eulerAngles.z + angle * rotationSpeed * Time.deltaTime);
		}
		base.Update ();
	}
}
