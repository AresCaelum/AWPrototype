using UnityEngine;
using System.Collections;

public class LivesManager : MonoBehaviour {

	public static LivesManager instance = null;
	[SerializeField]
	static private int maxLives = 5;
	static private int currentLives = 5;
	static private int lastTime = 0;

	[SerializeField]
	static private float lifeRegenRate = 300.0f;
	static private float lastHeartRegeneration = 0.0f;

	[SerializeField]
	static float saveTimerRate = 60.0f;

	[SerializeField]
	GameObject LifeAd;
	float saveTimer = 0.0f;

	System.DateTime now;
	// Use this for initialization
	void Start () {

		if (instance == null)
			instance = this;
		else
			Destroy (this);

		saveTimer = saveTimerRate;
		DontDestroyOnLoad (this);
		// algorithm is incorrect as there are 365 days a year this will give us 366, also it assumes there are 33 days a month, rather then 28 - 31.
		// However that little inaccuracy isn't going to negatively affect the player.  Instead it will help them.
		int currentTime = getSecondsNow();
		lastTime = PlayerPrefs.GetInt ("TimeClosed", 0);
		maxLives = PlayerPrefs.GetInt ("MaxLives", maxLives);
		currentLives = PlayerPrefs.GetInt ("PlayerLives", maxLives);
		lastHeartRegeneration = PlayerPrefs.GetFloat ("RegenTimer", 0.0f);
		Debug.Log (lastHeartRegeneration);
		float secondsPassed = (currentTime - lastTime);
		if (secondsPassed < 0) 
			Debug.Log ("Attempted to cheat...");
		
		lastHeartRegeneration -= secondsPassed;
		while (lastHeartRegeneration < 0.0f && currentLives < maxLives) {
			lastHeartRegeneration += lifeRegenRate;
			currentLives++;
		}

		if (currentLives >= maxLives) {
			lastHeartRegeneration = 0.0f;
			currentLives = maxLives;
		}
	}

	void Update()
	{
		int currentTime = getSecondsNow ();
		if (currentLives < maxLives) {
			lastHeartRegeneration -= (currentTime - lastTime);
			if (lastHeartRegeneration <= 0.0f) {
				currentLives++;
				if (currentLives < maxLives) {
					lastHeartRegeneration = lifeRegenRate + lastHeartRegeneration;
				} else {
					lastHeartRegeneration = 0.0f;
				}
			}
		}

		saveTimer -= Time.deltaTime;
		if (saveTimer < 0.0f) {
			instance.SavePrefs ();
			saveTimer = saveTimerRate + saveTimer;
		}

		if (Input.GetKeyUp (KeyCode.Minus)) {
			LostLife ();
		}

		lastTime = currentTime;
	}

	public bool CanPlay()
	{
		if (currentLives > 0) {
			return true;
		}

		Instantiate (LifeAd, Vector3.zero, Quaternion.identity);
		return false;
	}

	public void SavePrefs()
	{
		PlayerPrefs.SetInt ("TimeClosed", getSecondsNow());
		PlayerPrefs.SetInt ("MaxLives", maxLives);
		PlayerPrefs.SetInt ("PlayerLives", currentLives);
		PlayerPrefs.SetFloat ("RegenTimer", lastHeartRegeneration);

		PlayerPrefs.Save ();
	}

	void OnDestroy()
	{
		if (instance != null) {
			instance.SavePrefs ();
		}
	}

	public int GetLives()  {
		return currentLives;
	}

	public int GetMaxLives() {
		return maxLives;
	}

	public void LostLife()
	{
		currentLives--;
		if (currentLives < maxLives && !(lastHeartRegeneration > 0.0f))
			lastHeartRegeneration = lifeRegenRate;

		currentLives = Mathf.Max (currentLives, 0);
	}

	public void ResetLives()
	{
		currentLives = maxLives;
		lastHeartRegeneration = 0.0f;
	}

	public int getRegenMinute()
	{
		return (int)lastHeartRegeneration / 60;
	}

	public int getRegenSeconds()
	{
		return (int)lastHeartRegeneration % 60;
	}

	public string getRegenTimer()
	{
		if (currentLives < maxLives)
			return (getRegenMinute().ToString() + ":" + getRegenSeconds ().ToString ("D2"));
		return "";
	}

	int getSecondsNow()
	{
		now = System.DateTime.UtcNow;
		return ((now.Year - 2016) * 12 * 33 * 24 * 60) + (now.Month * 12 * 33 * 24 * 60) + (now.Day * 24 * 60) + (now.Hour * 60) + (now.Minute * 60) + (now.Second);
	}

	public string getLivesText()
	{
		return GetLives().ToString() + "/" + GetMaxLives().ToString() + "  " + getRegenTimer();
	}
}
