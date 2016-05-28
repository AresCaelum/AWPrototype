using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameMenu : MonoBehaviour {
	static GameMenu instance = null;
	[SerializeField]
	Image livesTemplate;
	[SerializeField]
	GameObject optionsbtn;
	[SerializeField]
	GameObject ScrollMenu;
	[SerializeField]
	float TitleTransitionTime = 0.0f;
	[SerializeField]
	float startY = 0.0f;
	[SerializeField]
	GameObject displayTemplate;
	[SerializeField]
	AudioClip MenuMusic;

	float TitleTimer = 0.0f;
	float EndY = 0;
	// Use this for initialization
	void Start () {
		if (instance != null) {
			Destroy (this.gameObject);
			return;
		}

		instance = this;

		if (DisplayTemplate.instance != null) {
			livesTemplate = DisplayTemplate.instance.Bar;
			TitleTimer = TitleTransitionTime;
			optionsbtn.gameObject.SetActive (true);
			ScrollMenu.SetActive (true);
		} else {
			GameObject temp = Instantiate (displayTemplate, Vector3.zero, Quaternion.identity) as GameObject;
			livesTemplate = temp.GetComponent<DisplayTemplate> ().Bar;
		}

		EndY = livesTemplate.rectTransform.anchoredPosition.y;
		if (MenuMusic != null) {
			AudioManager.PlayBGM (MenuMusic, false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (TitleTimer < TitleTransitionTime) {
			TitleTimer += Time.deltaTime;
			if (TitleTimer > TitleTransitionTime) {
				TitleTimer = TitleTransitionTime;
				optionsbtn.SetActive (true);
				ScrollMenu.SetActive (true);
			}
			livesTemplate.rectTransform.anchoredPosition = Vector2.Lerp(new Vector2(0, startY), new Vector2(0, EndY), TitleTimer / TitleTransitionTime);
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("MainMenu");
		}
	}
}
