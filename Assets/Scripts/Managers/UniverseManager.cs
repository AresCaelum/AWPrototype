using UnityEngine;
using System.Collections;

public class UniverseManager : MonoBehaviour {
	public static int universe_selected = 1;
	public static int world_selected = 1;

	void Start()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	static public void CompletedWorld()
	{
		PlayerPrefs.SetInt ("Universe_" + universe_selected.ToString() + "_" + world_selected.ToString (), 1);
		LivesManager.instance.SavePrefs ();
	}
}