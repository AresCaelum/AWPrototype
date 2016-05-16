using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {

	public void start_game()
	{
		GameManager.started_game = true;
		Destroy (this.gameObject);
	}
}
