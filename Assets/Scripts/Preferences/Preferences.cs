using UnityEngine;

public static class Preferences
{
    private const string MUSIC_VOL_KEY = "MusicVol";
    private const string SFX_VOL_KEY = "SfxVol";
    private const string MASTER_VOL_KEY = "MasterVol";

    static Preferences()
    {
        if (!PlayerPrefs.HasKey(MUSIC_VOL_KEY))
            PlayerPrefs.SetFloat(MUSIC_VOL_KEY, 50);

        if (!PlayerPrefs.HasKey(SFX_VOL_KEY))
            PlayerPrefs.SetFloat(SFX_VOL_KEY, 50);

        if (!PlayerPrefs.HasKey(MASTER_VOL_KEY))
            PlayerPrefs.SetFloat(MASTER_VOL_KEY, 50);

        PlayerPrefs.Save();
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOL_KEY);
    }
    public static void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MUSIC_VOL_KEY, volume);
    }

    public static float GetSfxVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOL_KEY);
    }
    public static void SetSfxVolume(float volume)
    {
        PlayerPrefs.SetFloat(SFX_VOL_KEY, volume);
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOL_KEY);
    }
    public static void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat(MASTER_VOL_KEY, volume);
    }

    public static void Save()
    {
        PlayerPrefs.Save();
    }
}


