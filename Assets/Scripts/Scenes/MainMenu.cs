using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {
	public AudioClip menuSound;
	[SerializeField]
	GameObject Tap;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			AudioManager.PlaySFX (menuSound);
			SceneManager.LoadScene ("GameMenu");
		}
	}

	public void ActivateTap()
	{
		Tap.SetActive (true);
	}
}
