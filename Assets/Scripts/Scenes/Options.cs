using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Options : MonoBehaviour {
	[SerializeField] Toggle displayTips;
	float timeScale = 0.0f;

	// Use this for initialization
	void Start () {
		timeScale = Time.timeScale;
		Time.timeScale = 0.0f;
		displayTips.isOn = (PlayerPrefs.GetInt ("HowTo", 0) != 1);
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
		int showTip = 1;
		if (displayTips.isOn)
			showTip = 0;
		PlayerPrefs.SetInt ("HowTo", showTip);
		Destroy (this.gameObject);
	}
}
