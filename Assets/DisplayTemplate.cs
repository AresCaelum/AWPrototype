using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayTemplate : MonoBehaviour {
	public Image Bar;
	public static DisplayTemplate instance = null;
	// Use this for initialization
	void Start () {
		if (instance != null)
			Destroy (this);

		instance = this;

		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
