using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	public void Resume()
	{
		GameManager.Handle_Pause ();
		Destroy (this.gameObject);
	}

	public void Exit()
	{
		LivesManager.instance.LostLife ();
		GameManager.reset ();
		SceneManager.LoadScene ("GameMenu");
	}

	public void Retry()
	{
		if (LivesManager.instance.CanPlay ()) {
			LivesManager.instance.LostLife ();
			GameManager.reset ();
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}
}
