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
		if (AdManager.instance != null)
			Destroy (this.gameObject);

		instance = this;
        DontDestroyOnLoad(this);
		#if UNITY_ANDROID
	        Advertisement.Initialize(androidID , true);
		#endif

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ShowAd()
    {
		if(Advertisement.IsReady(adZone))
        {
			ShowOptions options = new ShowOptions();
			options.resultCallback = RefreshLives;
			Advertisement.Show(adZone,options);
        }

    }

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
}
