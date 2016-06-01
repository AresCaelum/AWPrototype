using UnityEngine;
using System.Collections;

public class SkinSelection : MonoBehaviour {
	public Texture[] SkinSelections;
	[SerializeField] Sprite_Reskin myReskin; 
	int nCurrent = 0;
	// Use this for initialization
	void Start () {
		if (myReskin != null) {
			myReskin.SpriteSheet = PlayerPrefs.GetString ("PlayerSkin", "Player");
			int tempCurrent = 0;
			foreach (Texture tex in SkinSelections) {
				if (tex.name.Equals (myReskin.SpriteSheet)) {
					nCurrent = tempCurrent;
					break;
				}
				tempCurrent++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Ended) {
				Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.transform.gameObject == this.gameObject) {
						nCurrent++;
						if (nCurrent > SkinSelections.Length) {
							nCurrent = 0;
						}
					}
				}
			}
		}
		if (SkinSelections.Length > 0) {
			if (myReskin != null) {
				myReskin.SpriteSheet = SkinSelections[nCurrent].name;
			}
		}
	}

	void OnDestroy()
	{
		if (myReskin != null) {
			PlayerPrefs.SetString ("PlayerSkin", myReskin.SpriteSheet);
		}
	}

}
