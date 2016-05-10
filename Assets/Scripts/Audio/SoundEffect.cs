using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour {
	public AutoDeath setDeathTimer;
	public AudioSource myAudio;
	public AudioClip ClipToPlay;
	public float InitialVolume = 1.0f;
	public bool mute = false;
	bool oneShot = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	if (!oneShot) {
			myAudio.clip = ClipToPlay;
			myAudio.loop = false;
			myAudio.volume = InitialVolume;
			myAudio.mute = this.mute;
				myAudio.Play();
			setDeathTimer.LifeTime = ClipToPlay.length + 0.5f;
			oneShot = true;
		}
	}

	void OnDestroy()
	{
		if (AudioManager.instance != null) {
			AudioManager.RemoveAS(myAudio);
		}
	}
}
