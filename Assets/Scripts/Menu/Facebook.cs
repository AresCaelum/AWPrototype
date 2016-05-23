using UnityEngine;
using System.Collections;

public class Facebook : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void launchFacebook()
	{
		Application.OpenURL("https://www.facebook.com/TemperedGaming/");
	}
}
