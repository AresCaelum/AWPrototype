using UnityEngine;
using System.Collections;

public class GamePauseButton : MonoBehaviour {
	public GameObject pause_UI;

	public void pause_game()
	{
		GameManager.Handle_Pause ();
		Instantiate (pause_UI, Vector3.zero, Quaternion.identity);
	}
}
