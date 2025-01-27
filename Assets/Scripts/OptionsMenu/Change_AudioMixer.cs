using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Change_AudioMixer : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;
    public AudioMixerGroup CanalDeSonidoAManipular;

    [Range(0, 100)] public int linearNumber;
    public float logaritmicNumber;

    public void SetVolumeOnMixerScrollbar(float scrollbarValue)
    {
        linearNumber = Mathf.RoundToInt(scrollbarValue * 100);
        logaritmicNumber = LinearToDecibel(scrollbarValue * 100);
        audioMixer.SetFloat(CanalDeSonidoAManipular.name, logaritmicNumber);
    }

    public float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear / 10);
        else
            dB = -144.0f;

        return dB;
    }
}