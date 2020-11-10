using System.Collections;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    private AudioSource ambientPlayer;
    private AudioSource combatPlayer;

    void Start()
    {
        ambientPlayer = GameObject.FindGameObjectWithTag("AmbientMusic")?.GetComponent<AudioSource>();
        combatPlayer = GetComponent<AudioSource>();

        WaveController.OnWaveStarted += WaveController_OnWaveStarted;
        EnemySpawner.OnAllEnemiesKilled += EnemySpawner_OnAllEnemiesKilled;
    }

    private void EnemySpawner_OnAllEnemiesKilled()
    {
        if (ambientPlayer && combatPlayer)
        {
            StartCoroutine(StartFade(ambientPlayer, 1, 1));
            StartCoroutine(StartFade(combatPlayer, 1, 0));
        }
    }

    private void WaveController_OnWaveStarted(int currentWave)
    {
        if (ambientPlayer && combatPlayer)
        {
            StartCoroutine(StartFade(combatPlayer, 1, 1));
            StartCoroutine(StartFade(ambientPlayer, 1, 0));
        }
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}