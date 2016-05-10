using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Splash : MonoBehaviour {
	[SerializeField]
	float fadeInTimer;
	[SerializeField]
	float fadeOutTimer;
	[SerializeField]
	float stayTimer;
	[SerializeField]
	Image logo;

	float invFadeIn;
	float invFadeOut;
	// Use this for initialization
	void Start () {
		logo.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);

		invFadeIn = 1.0f / fadeInTimer;
		invFadeOut = 1.0f / fadeOutTimer;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.anyKeyDown) {
			SceneManager.LoadScene ("MainMenu");
		} else if (fadeInTimer > 0) {
			
			fadeInTimer -= Time.deltaTime;

			if (0 > fadeInTimer)
				fadeInTimer = 0;

			logo.color = new Color (1.0f, 1.0f, 1.0f, 1.0f - (fadeInTimer * invFadeIn));

		} else if (stayTimer > 0 ) {
			
			stayTimer -= Time.deltaTime;

		} else if (fadeOutTimer > 0) {
			
			fadeOutTimer -= Time.deltaTime;

			if (0 > fadeOutTimer)
				fadeOutTimer = 0;
			
			logo.color = new Color (1.0f, 1.0f, 1.0f, fadeOutTimer * invFadeOut);

		} else { 
			SceneManager.LoadScene ("MainMenu");
		}
	}

}
