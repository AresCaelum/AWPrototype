using UnityEngine;
using System.Collections;

public class RegenLives : MonoBehaviour {
	float timeScale = 0.0f;

	void Start()
	{
		timeScale = Time.timeScale;
	}
	public void closeMenu()
	{
		Destroy (this.gameObject);
	}

	public void LoadAd()
	{
		if(AdManager.instance != null)
			AdManager.instance.ShowAd ();
		Destroy (this.gameObject);
	}

	void OnDestroy()
	{
		Time.timeScale = timeScale;
	}
}
