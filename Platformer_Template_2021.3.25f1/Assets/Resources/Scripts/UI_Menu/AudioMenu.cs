using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

/// <summary>
///
/// License:
/// Copyrighted to Brian "VerzatileDev" Lätt ©2024.
/// Do not copy, modify, or redistribute this code without prior consent.
/// All rights reserved.
///
/// </summary>
/// <remarks>
/// Hosts buttons for Sliders and Labels to control the AudioMixer. < br />
/// Additionally, it saves the volume settings to PlayerPrefs.
/// </remarks>
public class AudioMenu : MonoBehaviour
{
    [System.Serializable]
    private class AudioProperties
    {
        public AudioMixer theMixer;
        [Space(1)]

        [Header("Sliders")]
        public Slider masterSlider = null;
        public Slider musicSlider = null;
        public Slider sfxSlider = null;
        [Space(1)]

        [Header("Labels")]
        public TMP_Text masterLabel = null;
        public TMP_Text musicLabel = null;
        public TMP_Text sfxLabel = null;
  
    }
    [SerializeField] private AudioProperties audioProperties;

    public void SetMasterVol()
    {
        audioProperties.masterLabel.text = Mathf.RoundToInt(audioProperties.masterSlider.value *100).ToString();
        audioProperties.theMixer.SetFloat("MasterValue", Mathf.Log10(audioProperties.masterSlider.value) *20);
    }
    public void SetMusicVol()
    {
        audioProperties.musicLabel.text = Mathf.RoundToInt(audioProperties.musicSlider.value *100).ToString();
        audioProperties.theMixer.SetFloat("MusicValue", Mathf.Log10(audioProperties.musicSlider.value) *20);
    }
    public void SetSFXVol()
    {
        audioProperties.sfxLabel.text = Mathf.RoundToInt(audioProperties.sfxSlider.value *100).ToString();
        audioProperties.theMixer.SetFloat("SfxValue", Mathf.Log10(audioProperties.sfxSlider.value) *20);
    }
    public void SaveVolumeButton()
    {
        PlayerPrefs.SetFloat("MasterValue", audioProperties.masterSlider.value);
        PlayerPrefs.SetFloat("MusicValue", audioProperties.musicSlider.value);
        PlayerPrefs.SetFloat("SfxValue", audioProperties.sfxSlider.value);

        PlayerPrefs.SetInt("VolumeSet", 1);
    }

    public void getAssignedValuesToSlider()
    {
        audioProperties.masterSlider.value = PlayerPrefs.GetFloat("MasterValue");
        audioProperties.musicSlider.value = PlayerPrefs.GetFloat("MusicValue");
        audioProperties.sfxSlider.value = PlayerPrefs.GetFloat("SfxValue");
    }
}