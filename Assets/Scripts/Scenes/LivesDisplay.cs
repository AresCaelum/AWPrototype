using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesDisplay : MonoBehaviour {
	public Text livesText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (LivesManager.instance != null) {
			livesText.text = LivesManager.instance.getLivesText ();
		}
	}
}
