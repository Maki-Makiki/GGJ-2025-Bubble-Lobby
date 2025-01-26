using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Player : MonoBehaviour
{
    public AudioSource efectsAudioSource;
    public AudioClip sfx_card_select;
    public AudioClip sfx_card_error;
    public AudioClip sfx_card_hover;
    public AudioClip sfx_card_draw;
    public AudioClip sfx_card_quick;
    public AudioClip sfx_card_shufle;

    private void Start()
    {
        Game_System.SetSingletone(this);
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        efectsAudioSource.PlayOneShot(audioClip);
    }

    public void PlayOneShot(soundNames audioName)
    {
        switch (audioName)
        {
            case soundNames.sfx_card_select:
                PlayOneShot(sfx_card_select);
                break;

            case soundNames.sfx_card_error:
                PlayOneShot(sfx_card_error);
                break;

            case soundNames.sfx_card_hover:
                PlayOneShot(sfx_card_hover);
                break;

            case soundNames.sfx_card_draw:
                PlayOneShot(sfx_card_draw);
                break;

            case soundNames.sfx_card_quick:
                PlayOneShot(sfx_card_quick);
                break;

            case soundNames.sfx_card_shufle:
                PlayOneShot(sfx_card_shufle);
                break;
        }
        
    }
}

public enum soundNames{
    sfx_card_select,
    sfx_card_error,
    sfx_card_hover,
    sfx_card_draw,
    sfx_card_quick,
    sfx_card_shufle,
}
