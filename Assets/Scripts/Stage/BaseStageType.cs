using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BaseStageType : MonoBehaviour {
	public int next_stage_id = 0;
	public bool last_stage = false;

	// Use this for initialization
	void Start () {
		GameManager.started_game = false;
		DefaultShot.numShot = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.started_game) {
			if (GameManager.enemy_count == 0) {
				if (next_stage_id != 0)
					SceneManager.LoadScene ("Universe_" + UniverseManager.universe_selected + "_" + UniverseManager.world_selected.ToString () + "_" + next_stage_id);
				else {
					if (last_stage)
						UniverseManager.CompletedWorld ();
					SceneManager.LoadScene ("GameMenu");
				}
			}
		}
	}
}
