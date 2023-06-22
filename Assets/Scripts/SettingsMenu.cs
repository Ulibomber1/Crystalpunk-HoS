using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Cinemachine;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;

    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";

    [SerializeField] private CinemachineFreeLook cmFreeLook;

    public TMPro.TMP_Dropdown resolutionDropdown;
    public Slider volume;
    public Toggle invertX;
    public Toggle invertY;
    public Slider sensitivity;
    public Slider FOVslider;
    public void passSettings() {
        SettingsData.volume = volume.value;
        SettingsData.invertX = invertX.isOn;
    }

    public delegate void SettingsAwakeHandler(SettingsMenu settingsMenu);
    public static event SettingsAwakeHandler onSettingsAwake;

    Resolution[] resolutions;

    private void Start()
    {
        MenuManager.SettingsExited += passSettings;
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + " Hz";
            options.Add(option);

            if (resolutions[i].Equals(Screen.currentResolution))
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Awake()
    {
/*        volume.value = SettingsData.volume;
        invertX.isOn = SettingsData.invertX;*/
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20);
    }

    public void SetSfxVolume(float volume)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void FOV(int fov)
    {
        cmFreeLook.m_Lens.FieldOfView = fov;
    }

    public void FlipX(bool flipX)
    {
        cmFreeLook.m_XAxis.m_InvertInput = flipX;
    }

    public void FlipY(bool flipY)
    {
        cmFreeLook.m_YAxis.m_InvertInput = flipY;
    }


    /*    public void Sensitivity(int fov)
        {
            cinemachineVirtualCamera.m_Lens.FieldOfView = fov;
        }*/

    /*    public void invertY(bool invert)
        {
            cinemachineVirtualCamera.GetCinemachineComponent<AxisControl>
        }*/
}
