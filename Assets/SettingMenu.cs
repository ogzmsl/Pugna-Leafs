using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public GameObject Panel;
    public AudioMixer audioMixer;
    public TMP_Dropdown reseulationdropdown;

    Resolution[] resolutions;

    void Start()
    {
        Panel.SetActive(false);
        resolutions = Screen.resolutions;
        reseulationdropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResulationIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResulationIndex = i;
            }
        }
        reseulationdropdown.AddOptions(options);

       
        currentResulationIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResulationIndex);

        reseulationdropdown.value = currentResulationIndex;
        reseulationdropdown.RefreshShownValue();

      
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.75f); 
        SetVolume(savedVolume);

        int savedQuality = PlayerPrefs.GetInt("Quality", QualitySettings.GetQualityLevel());
        SetQuality(savedQuality);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetQuality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
        PlayerPrefs.SetInt("Quality", QualityIndex);
    }

    public void setResulation(int resulationIndex)
    {
        Resolution resolution = resolutions[resulationIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resulationIndex);
    }

    public void SettingPanelOn()
    {
        Panel.SetActive(true);
    }

    public void SettinPanelOff()
    {
        Panel.SetActive(false);
    }
}
