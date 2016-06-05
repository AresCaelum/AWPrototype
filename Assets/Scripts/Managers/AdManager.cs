using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdManager : MonoBehaviour
{
	static public AdManager instance = null;
    [SerializeField]
    string androidID = "";
	[SerializeField]
	string iphoneID = "";
	[SerializeField]
	string adZone = null;

    // Use this for initialization
    void Start()
    {
		if (AdManager.instance != null) {
			Destroy (this.gameObject);
			return;
		}

		instance = this;
        DontDestroyOnLoad(this);
		#if UNITY_ANDROID
	        Advertisement.Initialize(androidID , true);
		#elif UNITY_IOS
			Advertisement.Initialize(iphoneID , true);
		#endif

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ShowAd()
    {
		#if UNITY_ADS
		if(Advertisement.IsReady(adZone))
        {
			ShowOptions options = new ShowOptions();
			options.resultCallback = RefreshLives;
			Advertisement.Show(adZone,options);
        }
		#else
			LivesManager.instance.ResetLives ();
		#endif
    }

	#if UNITY_ADS
	public void RefreshLives(ShowResult result)
	{
		switch (result) {
			case ShowResult.Failed:
				break;
			case ShowResult.Skipped:
				break;
		case ShowResult.Finished:
			LivesManager.instance.ResetLives ();
			break;
		}
	}
	#endif
}
