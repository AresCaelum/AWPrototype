using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class WorldSelect : MonoBehaviour {
	[SerializeField]
	int world = 1;

	// Use this for initialization
	void Start () {

		if (world == 1)
			return;	
		
		if(PlayerPrefs.GetInt("Universe_" + UniverseManager.universe_selected.ToString() + "_" + (world-1).ToString(), 0) == 0)
		{
			gameObject.SetActive(false);
		}
	}

	public void selectWorld()
	{

		DragHandler dh = GetComponent<DragHandler>();
		if(dh)
		{
			// Has DragHandler
			if(!dh.IsDragging())
			{
				if (LivesManager.instance.CanPlay ()) {
					UniverseManager.world_selected = world;
					SceneManager.LoadScene ("Universe_" + UniverseManager.universe_selected + "_" + world.ToString () + "_1");
				}
			}
		}

	}
}
