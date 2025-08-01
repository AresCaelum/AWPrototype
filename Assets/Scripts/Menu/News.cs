﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class News : MonoBehaviour {
	[SerializeField] Text newText;
	[SerializeField] RectTransform contentSize;
	[SerializeField] ScrollRect scrollHandle;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("NewsClose", 0) == System.DateTime.Now.Day) {
			Destroy (this.gameObject);
			return;
		}
		//StartCoroutine (getNews());

		// Temporary until I can figure out why I can't upload txt and html documents
		newText.text = 	"May 30, 2016:\n" +
						"-------------\n" +
						"Added a skin for the main character. (Tap the frog on the main menu).\n" +
						"Added Functionality to ensure the play area that is visible in the camera is same across multiple devices.\n" +
						"New Boss added.\n" +
						"New PowerUp - MultiShot added.\n\n" +
						"May 23, 2016: Update 2\n" +
						"----------------------" +
						"Added UI to show cooldown on weapons.\n" +
						"Added new defense powerup placed on stage 5.\n" +
						"Removed some extra powerups.\n\n" +
						"May 23, 2016:\n" +
						"-------------\n" +
						"Added:\n" +
						"This news system.\n" +
						"Facebook button.";
		scrollHandle.verticalNormalizedPosition = 1;
	}
	
	// Update is called once per frame
	void Update () {

	}


	IEnumerator getNews()
	{
		WWW newsPage = new WWW ("https://static.wixstatic.com/ugd/b00fc8_c10e2e5a375a41d2a101d9dff03a4bf7.docx");
		yield return newsPage;
		newText.text = newsPage.text;
		contentSize.sizeDelta = new Vector2 (contentSize.sizeDelta.x, newText.preferredHeight + 16);
		scrollHandle.verticalNormalizedPosition = 1;
	}

	public void Close()
	{
		PlayerPrefs.SetInt ("NewsClose", System.DateTime.Now.Day);
		Destroy (this.gameObject);
	}
}
