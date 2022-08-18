using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI fullScreenText;
    public Slider audioSlider;

    public void Start()
    {
        panel.SetActive(false);
        if(PlayerPrefs.HasKey("GlobalVolume"))
        {
            float val = PlayerPrefs.GetFloat("GlobalVolume");
            audioSlider.value = val;
            AudioListener.volume = val;
        }

        if (PlayerPrefs.HasKey("ScreenType"))
        {
            FullScreenMode fullScreenMode = (FullScreenMode)System.Enum.Parse(typeof(FullScreenMode), PlayerPrefs.GetString("ScreenType"));
            Screen.fullScreenMode = fullScreenMode;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

    public void UpdateFullScreenMode()
    {
        FullScreenMode fullScreenMode = (FullScreenMode) System.Enum.Parse(typeof(FullScreenMode), fullScreenText.text.Trim());
        Screen.fullScreenMode = fullScreenMode;
        PlayerPrefs.SetString("ScreenType", fullScreenMode.ToString());
        PlayerPrefs.Save();
    }

    public void UpdateAudio()
    {
        AudioListener.volume = audioSlider.value;
        PlayerPrefs.SetFloat("GlobalVolume", AudioListener.volume);
        PlayerPrefs.Save();
    }
}
