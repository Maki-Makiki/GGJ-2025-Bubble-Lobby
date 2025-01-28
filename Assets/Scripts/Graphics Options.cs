using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GraphicsOptions : MonoBehaviour
{
    public bool debug = false;
    [Space]
    public bool setupReady = false;
    [Space]
    public TMP_Dropdown graphicsDropdown;
    [Space]
    public int SettingNum;
    public int defaultValue;
    


    public void Start()
    {
        SetDropdown();
        SetDropdownGraphics();
    }

    public void SetDropdown()
    {
        if (graphicsDropdown == null)
            graphicsDropdown = this.GetComponent<TMP_Dropdown>();
    }

    public void SetDropdownGraphics()
    {
        SettingNum = LoadPlayerPrefData();
        if (graphicsDropdown.value == SettingNum)
        {
            graphicsDropdown.onValueChanged.Invoke(0);
        }
        else
        {
            graphicsDropdown.value = SettingNum;
        }
    }

    public void ReadyGraphicsController(int value)
    {
        if (value == 0 && setupReady)
        {
            SettingNum = graphicsDropdown.value;
            SavePlayerPrefData();
        }
        else { setupReady = true; }
    }

    public void CambiarSombras(int value = 0)
    {
        ReadyGraphicsController(value);

        //Seteamos las sombras en encendido
        UnityGraphicsBullshit.MainLightCastShadows = SettingNum == 0? false : true;
        UnityGraphicsBullshit.MainLightShadowResolution = Graf.shadowMapRes[SettingNum];

        //se setea de esta forma para no necesitar un switch
        QualitySettings.shadowResolution = Graf.shadowRes[SettingNum];
        QualitySettings.shadows = Graf.shadowQuality[SettingNum];

        debugMesage("se cambiaron las sombras a " + Graf.tipoSombra[SettingNum]);
    }

    public void CambiarAntialiasing(int value = 0)
    {
        ReadyGraphicsController(value);

        UnityGraphicsBullshit.MSAA_Quality = Graf.MSAAQuality[SettingNum];

        debugMesage("se cambio el antialiasing a MSAA" + Graf.MSAAQuality[SettingNum]);
    }

    public void CambiarFPS(int value = 0)
    {
        ReadyGraphicsController(value);

        QualitySettings.vSyncCount = 1;
        int fpsCount = 0;
        int.TryParse(graphicsDropdown.options[graphicsDropdown.value].text, out fpsCount);
        Application.targetFrameRate = fpsCount;

        Screen.SetResolution(
            Screen.currentResolution.width, 
            Screen.currentResolution.height, 
            Screen.fullScreenMode, 
            new RefreshRate() { numerator = (uint)fpsCount, denominator = 1 });

        debugMesage("se cambio el frame cap a " + fpsCount + " FPS");

    }

    public void CambiarVentana(int value = 0)
    {
        ReadyGraphicsController(value);

        Screen.SetResolution(
            Screen.currentResolution.width, 
            Screen.currentResolution.height,
            Graf.fullscreenMode[SettingNum], 
            Screen.currentResolution.refreshRateRatio);

        debugMesage("se cambio el tipo de ventana a " + Graf.fullscreenMode[SettingNum] + "");
    }

    public void CambiarResolucion(int value = 0)
    {
        ReadyGraphicsController(value);

        string[] dropdownText = graphicsDropdown.options[graphicsDropdown.value].text.Split(" x ");
        int width = 0;
        int.TryParse(dropdownText[0], out width);
        int height = 0;
        int.TryParse(dropdownText[0], out height);

        Screen.SetResolution(
            width,
            height,
            Screen.fullScreenMode,
            Screen.currentResolution.refreshRateRatio);

        debugMesage("se cambio la resolución a " + width + " x " + height);
    }

    public int LoadPlayerPrefData()
    {
        return PlayerPrefs.GetInt(getConfigType(), defaultValue);
    }

    public void SavePlayerPrefData()
    {
        PlayerPrefs.SetInt(getConfigType(), SettingNum);
    }

    public string getConfigType()
    {
        return graphicsDropdown.onValueChanged.GetPersistentMethodName(0)
            .Replace("Cambiar", "");
    }

    public void debugMesage(string message)
    {
        if (debug)
        {
            Debug.Log(message);
        }
    }
}

public static class Graf
{
    public static string[] tipoSombra = new string[]
    {
        "Off",
        "Bajo",
        "Medio",
        "Alto",
        "Ultra"
    };

    public static List<UnityEngine.Rendering.Universal.ShadowResolution>
        shadowMapRes = new List<UnityEngine.Rendering.Universal.ShadowResolution>()
    {
        UnityEngine.Rendering.Universal.ShadowResolution._256,
        UnityEngine.Rendering.Universal.ShadowResolution._512,
        UnityEngine.Rendering.Universal.ShadowResolution._1024,
        UnityEngine.Rendering.Universal.ShadowResolution._2048,
        UnityEngine.Rendering.Universal.ShadowResolution._4096
    };

    public static List<ShadowResolution> shadowRes = new List<ShadowResolution>()
    {
        ShadowResolution.Low,
        ShadowResolution.Medium,
        ShadowResolution.Medium,
        ShadowResolution.High,
        ShadowResolution.VeryHigh

    };

    public static List<ShadowQuality> shadowQuality = new List<ShadowQuality>()
    {
        ShadowQuality.Disable,
        ShadowQuality.HardOnly,
        ShadowQuality.All,
        ShadowQuality.All,
        ShadowQuality.All

    };

    public static List<UnityEngine.Rendering.Universal.MsaaQuality>
        MSAAQuality = new List<UnityEngine.Rendering.Universal.MsaaQuality>
    {
            UnityEngine.Rendering.Universal.MsaaQuality.Disabled,
            UnityEngine.Rendering.Universal.MsaaQuality._2x,
            UnityEngine.Rendering.Universal.MsaaQuality._4x,
            UnityEngine.Rendering.Universal.MsaaQuality._8x,
    };

    public static List<FullScreenMode> fullscreenMode = new List<FullScreenMode>()
    {
        FullScreenMode.Windowed,
        FullScreenMode.MaximizedWindow,
        FullScreenMode.ExclusiveFullScreen
    };

}