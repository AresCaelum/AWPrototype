using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

	float timeScale = 0.0f;

	// Use this for initialization
	void Start () {
		timeScale = Time.timeScale;
		Time.timeScale = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Destroy (this.gameObject);
		}
	}

	void OnDestroy()
	{
		AudioManager.SavePrefs ();
		Time.timeScale = timeScale;
	}

	public void CloseOptions()
	{
		Destroy (this.gameObject);
	}
}
