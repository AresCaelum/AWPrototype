using UnityEngine;
using System.Collections;

public class AutoDeath : MonoBehaviour {
	public float LifeTime = 100.0f;
	public bool DeathActive = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (DeathActive) {
			LifeTime -= Time.deltaTime;

		}
		if (LifeTime <= 0.0f) {
			Destroy (this.gameObject);
		}
	}
}
