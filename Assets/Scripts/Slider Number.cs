using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SliderNumber : MonoBehaviour
{

    public float defaultVolume = 100;
    public float SavedVolume;
    [Header("Setuo Elements")]
    public Scrollbar scrollbar;
    public TMP_Text numeroText;
    [Space]
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;
    public AudioMixerGroup CanalDeSonidoAManipular;
    [Space]
    [Header("Debug Values")]
    public float linearNumber = 0;
    public float logaritmicNumber = 0;
    [Space]
    public float debugAudioMixer;

    public void Start()
    {
        
        AsignarBarraVol();
        CambiarSonido();
        string CanalName = CanalDeSonidoAManipular.name;
        audioMixer.GetFloat(CanalName, out debugAudioMixer);
    }

    public void AsignarElementos()
    {
        if (scrollbar == null) 
        {
            scrollbar = GetComponent<Scrollbar>();
        }

        if (numeroText == null)
        {
            numeroText = 
                transform
                .GetChild(0)
                    .GetChild(0)
                        .GetChild(0).gameObject.GetComponent<TMP_Text>();
        }
    }

    public void AsignarBarraVol()
    {
        AsignarElementos();
        LoadVolume();
        scrollbar.value = SavedVolume / 100;
    }

    public void AsignarNumero()
    {
        numeroText.text = Mathf.RoundToInt(scrollbar.value * 100).ToString();
    }

    public void CambiarSonido()
    {
        AsignarElementos();
        logaritmicNumber = LinearToDecibel(scrollbar.value*10);
        audioMixer.SetFloat(CanalDeSonidoAManipular.name, logaritmicNumber);
        SaveVolumen();
        AsignarNumero();
    }

    public void SaveVolumen()
    {
        PlayerPrefs.SetFloat(
            CanalDeSonidoAManipular.name + "_vol", scrollbar.value * 100);
    }

    public void LoadVolume()
    {
        SavedVolume = PlayerPrefs.GetFloat(
            CanalDeSonidoAManipular.name + "_vol", defaultVolume);
    }

    public float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
            dB = 35.0f * Mathf.Log10(linear / 10);
        else
            dB = -144.0f;

        return dB;
    }
}

public enum TipoSonido
{
    Master,
    Musica,
    Efectos
}