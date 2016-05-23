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
		if(GameManager.started_game)
			LivesManager.instance.LostLife ();
		GameManager.reset ();
		SceneManager.LoadScene ("GameMenu");
	}

	public void Retry()
	{
		if (LivesManager.instance.CanPlay ()) {
			if(GameManager.started_game)
				LivesManager.instance.LostLife ();
			GameManager.reset ();
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}
}
