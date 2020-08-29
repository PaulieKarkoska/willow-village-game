using UnityEngine;

public static class Preferences
{
    private const string MUSIC_VOL_KEY = "MusicVol";
    private const string SFX_VOL_KEY = "SfxVol";

    static Preferences()
    {
        if (!PlayerPrefs.HasKey(MUSIC_VOL_KEY))
            PlayerPrefs.SetFloat(MUSIC_VOL_KEY, 1);

        if (!PlayerPrefs.HasKey(SFX_VOL_KEY))
            PlayerPrefs.SetFloat(SFX_VOL_KEY, 1);
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


    public static void Save()
    {
        PlayerPrefs.Save();
    }
}


