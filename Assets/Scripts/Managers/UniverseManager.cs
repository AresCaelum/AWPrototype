using UnityEngine;
using System.Collections;

public class UniverseManager : MonoBehaviour {
	public static int universe_selected = 1;
	public static int world_selected = 1;
	static UniverseManager instance;

	void Start()
	{
		if (instance != null) {
			Destroy (this.gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad (this.gameObject);
	}

	static public void CompletedWorld()
	{
		PlayerPrefs.SetInt ("Universe_" + universe_selected.ToString() + "_" + world_selected.ToString (), 1);
		LivesManager.instance.SavePrefs ();
	}

	void OnDestroy()
	{
		if (instance == this) {
			instance = null;
		}
	}
}