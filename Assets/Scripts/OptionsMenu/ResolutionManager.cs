using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] _resolutions;
    private List<Resolution> _filteredResolutions;

    private float _currentRefreshRate;
    private int _currentResolutionIndex = 0;

    void Start()
    {
        _resolutions = Screen.resolutions;
        _filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        _currentRefreshRate = Screen.currentResolution.refreshRate;

        foreach (var t in _resolutions)
        {
            if (t.refreshRate == _currentRefreshRate)
            {
                _filteredResolutions.Add(t);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < _filteredResolutions.Count; i++)
        {
            string resolutionOption = _filteredResolutions[i].width + "x" + _filteredResolutions[i].height;
            options.Add(resolutionOption);
            if (_filteredResolutions[i].width == Screen.width && _filteredResolutions[i].height == Screen.height)
            {
                _currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = _currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
        Debug.Log("Current resolution: " + resolution.width + "x" + resolution.height);
        PlayerPrefs.SetInt("screenWidth", resolution.width);
        Debug.Log("Player prefs - screenWidth: " + PlayerPrefs.GetInt("screenWidth"));
        PlayerPrefs.SetInt("screenHeight", resolution.height);
        Debug.Log("Player prefs - screenHeight: " + PlayerPrefs.GetInt("screenHeight"));
        PlayerPrefs.SetInt("screenRefreshRate", Screen.currentResolution.refreshRate);
        Debug.Log("Player prefs - screenRefreshRate: " + PlayerPrefs.GetInt("screenRefreshRate"));
    }
}