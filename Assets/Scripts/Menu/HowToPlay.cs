using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HowToPlay : MonoBehaviour {
	enum HowToState {MOVE_LEFT, MOVE_RIGHT, SHOOT}
	public GameObject CountDownObject;
	public GameObject moveLeft;
	public GameObject moveRight;
	public GameObject shootState;
	public Toggle checkedObject;

	HowToState myState = HowToState.MOVE_LEFT;

	void Start()
	{
		if (PlayerPrefs.GetInt ("HowTo", 0) == 1) {
			Instantiate (CountDownObject, Vector3.zero, Quaternion.identity);
			Destroy (this.gameObject);
		}
	}

	public void beginPlay()
	{
		if (checkedObject.isOn) {
			PlayerPrefs.SetInt ("HowTo", 1);
		}

		Instantiate (CountDownObject, Vector3.zero, Quaternion.identity);
		Destroy (this.gameObject);
	}

	public void NextTransition()
	{
		switch (myState) {
		case HowToState.MOVE_LEFT:
			{
				moveRight.SetActive (true);
				moveLeft.SetActive (false);
				myState = HowToState.MOVE_RIGHT;
				break;
			}
		case HowToState.MOVE_RIGHT:
			{
				shootState.SetActive (true);
				moveRight.SetActive (false);
				myState = HowToState.SHOOT;
				break;
			}
		case HowToState.SHOOT:
			{
				shootState.SetActive (false);
				moveLeft.SetActive (true);
				myState = HowToState.MOVE_LEFT;
				break;
			}
		}
	}
}
