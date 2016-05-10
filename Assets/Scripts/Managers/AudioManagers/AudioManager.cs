using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	static public AudioManager instance = null;
	static public float MasterVolume = 1.0f;
	static public float MusicVolume = 1.0f;
	static public float SoundEffectVolume = 1.0f;
	static public bool Muted = false;
	public GameObject OneShotAudio;
	public AudioSource BackgroundMusic; 

	public List<AudioSource> mySFX = new List<AudioSource>();
	AudioClip TransitionTo;
	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else
			Destroy (this.gameObject);

		DontDestroyOnLoad (this);
		MasterVolume = PlayerPrefs.GetFloat ("masterVolume", 1.0f);
		MusicVolume = PlayerPrefs.GetFloat ("musicVolume", 1.0f);
		SoundEffectVolume = PlayerPrefs.GetFloat ("sfxVolume", 1.0f);

		BackgroundMusic.volume = MusicVolume * MasterVolume;
	}

	// Update is called once per frame
	void Update () {
	}
	static public void SetMasterVolume(float _volume)
	{
		MasterVolume = Mathf.Min(1.0f, Mathf.Max(0.0f, _volume));
		instance.BackgroundMusic.volume = MusicVolume * MasterVolume;
		foreach (AudioSource AS in instance.mySFX) {
			AS.volume = SoundEffectVolume * MasterVolume;;
		}
	}
	static public void SetMusicVolume(float _volume)
	{
		MusicVolume = Mathf.Min(1.0f, Mathf.Max(0.0f, _volume));
		instance.BackgroundMusic.volume = MusicVolume * MasterVolume;
	}
	static public void SetSoundEffectVolume(float _volume)
	{
		SoundEffectVolume = Mathf.Min(1.0f, Mathf.Max(0.0f, _volume));
		foreach (AudioSource AS in instance.mySFX) {
			if(AS != null)
				AS.volume = SoundEffectVolume * MasterVolume;
		}
	}
	static public void Mute(bool _value)
	{
		Muted = _value;
		instance.BackgroundMusic.mute = _value;
		foreach (AudioSource AS in instance.mySFX) {
			if(AS != null)
				AS.mute = _value;
		}
	}
	static public void PlayBGM(AudioClip _Clip)
	{
		if (instance.BackgroundMusic.isPlaying)
			instance.BackgroundMusic.Stop ();
		instance.BackgroundMusic.clip = _Clip;
		instance.BackgroundMusic.volume = MusicVolume * MasterVolume;
		instance.BackgroundMusic.Play ();
	}
	static public void TransitionBGM(AudioClip newClip)
	{

	}

	static public void RemoveAS(AudioSource _AS)
	{
		instance.mySFX.Remove (_AS);
	}

	static public void PlaySFX(AudioClip _SFX)
	{
		if (!Muted) {
			GameObject Temp = Instantiate (instance.OneShotAudio, instance.transform.position, Quaternion.identity) as GameObject;
			SoundEffect TempSound = Temp.GetComponent<SoundEffect> ();
			TempSound.ClipToPlay = _SFX;
			TempSound.InitialVolume = SoundEffectVolume * MasterVolume;
			TempSound.mute = Muted;
			instance.mySFX.Add (Temp.GetComponent<AudioSource> ());
		}
	}

	static public void SavePrefs()
	{
		PlayerPrefs.SetFloat ("masterVolume", MasterVolume);
		PlayerPrefs.SetFloat ("musicVolume", MusicVolume);
		PlayerPrefs.SetFloat ("sfxVolume", SoundEffectVolume);

		PlayerPrefs.Save ();
	}

	void OnDestroy()
	{
		if (instance != null)
			SavePrefs ();
	}
}
