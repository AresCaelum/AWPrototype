using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {
	public AudioClip menuSound;
	[SerializeField]
	Image tap;
	[SerializeField]
	Image Title;

	[SerializeField]
	float flashSpeed;
	[SerializeField]
	float TitleTransitionTime = 0.0f;
	[SerializeField]
	float startY = -3000.0f;

	float TitleTimer = 0.0f;
	float totalTime = 0;

	float EndY = 0;

	// Use this for initialization
	void Start () {
		EndY = Title.rectTransform.anchoredPosition.y;
		tap.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);

	}
	
	// Update is called once per frame
	void Update () {
		if (TitleTimer < TitleTransitionTime) {
			TitleTimer += Time.deltaTime;
			if (TitleTimer > TitleTransitionTime) {
				TitleTimer = TitleTransitionTime;
				tap.gameObject.SetActive (true);
			}
			Title.rectTransform.anchoredPosition = Vector2.Lerp(new Vector2(0, startY), new Vector2(0, EndY), TitleTimer / TitleTransitionTime);
		}
		else
		{
			totalTime += Time.deltaTime;

			tap.color = new Color (1.0f, 1.0f, 1.0f, 1.0f - Mathf.Sin(totalTime * flashSpeed));

			if (Input.anyKeyDown) {
				AudioManager.PlaySFX (menuSound);
				SceneManager.LoadScene ("GameMenu");
			}
		}
	}

}
