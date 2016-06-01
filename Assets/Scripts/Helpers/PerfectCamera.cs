using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PerfectCamera : MonoBehaviour {
	public float pixelPerUnit = 100;
	public int targetWidth = 2560;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		int height = Mathf.RoundToInt (targetWidth / (float)Screen.width * Screen.height);
		Camera.main.orthographicSize = height / pixelPerUnit * 0.5f;
	}
}
