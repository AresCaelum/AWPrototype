using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackToGameMenu : MonoBehaviour {

	public void ToGameMenu()
	{
		SceneManager.LoadScene ("GameMenu");
	}
}
