using UnityEngine;
using System.Collections;

public class OptionsButton : MonoBehaviour {
	public AudioClip menuSound = null;
	public GameObject optionsScreen;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadOptions()
	{
		if(optionsScreen != null)
		{
			if(menuSound != null)
				AudioManager.PlaySFX (menuSound);
			Instantiate(optionsScreen, Vector3.zero, Quaternion.identity);
		}
	}
}
