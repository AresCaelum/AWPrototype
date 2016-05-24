using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathMenu : MonoBehaviour {
	public void Exit()
	{
		GameManager.reset ();
		SceneManager.LoadScene ("GameMenu");
	}

	public void Retry()
	{
		if (LivesManager.instance.CanPlay ()) {
			GameManager.reset ();
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}
}
