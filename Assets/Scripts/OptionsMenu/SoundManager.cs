using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioMixerGroup musicGroup;

    public void SetVolume(float sliderValue)
    {
        mixer.SetFloat(musicGroup.name, Mathf.Log10(sliderValue) * 20);
    }
}