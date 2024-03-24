using UnityEngine;
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
/// Loads the player's audio settings from PlayerPrefs and sets them to the AudioMixer. < br />
/// Get and Set Default Audio Settings to PlayerPrefs and AudioMixer.
/// </remarks>
public class LoadPlayerAudio : MonoBehaviour
{
    public AudioMixer theMixer;
    private protected float DefaultMasterValue = 1.0f;
    private protected float DefaultMusicValue = 0.5f;
    private protected float DefaultSfxValue = 0.5f;

    private void Start()
    {
        if (PlayerPrefs.HasKey("VolumeSet") & PlayerPrefs.GetInt("VolumeSet") == 1)
        {
            GetPlayerGameAudioToMixer();
        }
        else
        {
            SetDefaultGameAudioSettingsToPlayerPrefs();
            SetDefaultGameAudioToMixer();
        }
    }

    public void SetDefaultGameAudioSettingsToPlayerPrefs()
    {
        PlayerPrefs.SetInt("VolumeSet", 0); // Stating the Values are Un-set
        PlayerPrefs.SetFloat("MasterValue", DefaultMasterValue);
        PlayerPrefs.SetFloat("MusicValue", DefaultMusicValue);
        PlayerPrefs.SetFloat("SfxValue", DefaultSfxValue);
    }

    public void SetDefaultGameAudioToMixer()
    {
        theMixer.SetFloat("MasterValue", Mathf.Log10(DefaultMasterValue) * 20);
        theMixer.SetFloat("MusicValue", Mathf.Log10(DefaultMusicValue) * 20);
        theMixer.SetFloat("SfxValue", Mathf.Log10(DefaultSfxValue) * 20);
    }

    public void GetPlayerGameAudioToMixer()
    {
        theMixer.SetFloat("MasterValue", Mathf.Log10(PlayerPrefs.GetFloat("MasterValue")) * 20);
        theMixer.SetFloat("MusicValue", Mathf.Log10(PlayerPrefs.GetFloat("MusicValue")) * 20);
        theMixer.SetFloat("SfxValue", Mathf.Log10(PlayerPrefs.GetFloat("SfxValue")) * 20);
    }
}
