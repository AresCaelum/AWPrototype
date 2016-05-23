using UnityEngine;
using System.Collections;

public class GameManager{
	static public bool paused = false;
	static public int enemy_count = 0;
	static public bool started_game = false;

	public static void Handle_Pause()
	{
		paused = !paused;
		if (paused) {
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}
	}

	public static void reset()
	{
		started_game = false;
		paused = false;
		enemy_count = 0;
		Time.timeScale = 1.0f;
	}

}
