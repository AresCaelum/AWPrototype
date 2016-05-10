using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UniverseSelect : MonoBehaviour {
	[SerializeField]
	int universeToLoad = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadUniverse()
	{
		DragHandler dh = GetComponent<DragHandler>();
		if(dh)
		{
			// Has DragHandler
			if(!dh.IsDragging())
			{
				UniverseManager.universe_selected = universeToLoad;
				SceneManager.LoadScene ("Universe_" + universeToLoad.ToString ());
			}
		}
	}
}
