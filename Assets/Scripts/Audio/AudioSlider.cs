using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public enum SOUNDSETTINGS { MASTER_VOL, MUSIC_VOL, SFX_VOL, TOTAL_VOL };

public class AudioSlider : MonoBehaviour
{
    public Slider mySlider;
    public SOUNDSETTINGS Effect;

    float lastPosition;
    // Use this for initialization
    void Start()
    {
        switch (Effect)
        {
            case SOUNDSETTINGS.MASTER_VOL:
            {
                    mySlider.value = AudioManager.MasterVolume;
                    break;
            }
            case SOUNDSETTINGS.MUSIC_VOL:
            {
                    mySlider.value = AudioManager.MusicVolume;
                    break;
            }
            case SOUNDSETTINGS.SFX_VOL:
            {
                    mySlider.value = AudioManager.SoundEffectVolume;
                    break;
            }
            default:
                break;
        }

        lastPosition = mySlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if(mySlider.value != lastPosition)
        {
            if(AudioManager.Muted)
            {
                AudioManager.Mute(false);
            }
            switch (Effect)
            {
                case SOUNDSETTINGS.MASTER_VOL:
                    {
                        AudioManager.SetMasterVolume(mySlider.value);
                        break;
                    }
                case SOUNDSETTINGS.MUSIC_VOL:
                    {
                        AudioManager.SetMusicVolume(mySlider.value);
                        break;
                    }
                case SOUNDSETTINGS.SFX_VOL:
                    {
                        AudioManager.SetSoundEffectVolume(mySlider.value);
                        break;
                    }
                default:
                    break;
            }
            lastPosition = mySlider.value;
        }
    }
}
