using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [Header("Value Displays")]
    [SerializeField]
    private TextMeshProUGUI _musicVolDisplay;
    [SerializeField]
    private TextMeshProUGUI _sfxVolDisplay;


    public void UpdateMusicVol(float vol)
    {
        Preferences.SetMusicVolume(vol);
        _musicVolDisplay.text = $"{Mathf.RoundToInt(vol * 100)}%";
    }

    public void UpdateSfxVol(float vol)
    {
        Preferences.SetSfxVolume(vol);
        _sfxVolDisplay.text = $"{Mathf.RoundToInt(vol * 100)}%";
    }

    public void Save()
    {
        Preferences.Save();
    }
}
