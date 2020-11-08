using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    public static SfxPlayer instance { get; private set; }
    public AudioSource source { get; private set; }

    private void Start()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip, bool cancelCurrent = false)
    {
        if (!source.isPlaying || cancelCurrent)
        {
            source.clip = clip;
            source.Play();
        }
    }
}