using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuickMute : MonoBehaviour
{
    public Toggle myToggle;
    // Use this for initialization
    void Start()
    {
        myToggle.isOn = AudioManager.Muted;
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioManager.Muted != myToggle.isOn)
            myToggle.isOn = AudioManager.Muted;
    }

    public void UpdateMute()
    {
        AudioManager.Mute(myToggle.isOn);

    }
}
