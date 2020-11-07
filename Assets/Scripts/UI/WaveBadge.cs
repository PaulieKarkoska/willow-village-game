using System.Collections;
using TMPro;
using UnityEngine;

public class WaveBadge : MonoBehaviour
{
    private WaveController wController;

    [SerializeField]
    private RectTransform badge;
    [SerializeField]
    private TextMeshProUGUI waveNumberText;
    [SerializeField]
    private RectTransform timer;
    [SerializeField]
    private TextMeshProUGUI countdownText;

    [SerializeField]
    private AudioClip newWaveClip;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wController = GameObject.Find("WaveController").GetComponent<WaveController>();
        WaveController.OnTimerUpdated += WaveController_OnTimerUpdated;
        WaveController.OnWaveStarted += WaveController_OnWaveStarted;
        EnemySpawner.OnAllEnemiesKilled += EnemySpawner_OnAllEnemiesKilled;
        WaveController.OnCountdownStarted += WaveController_OnCountdownStarted;
    }

    private void WaveController_OnCountdownStarted()
    {
        LeanTween.moveY(timer, -20, 1);
    }

    private void EnemySpawner_OnAllEnemiesKilled()
    {
        StartCoroutine(WaveEnd());
    }

    private void WaveController_OnWaveStarted(int currentWave)
    {
        waveNumberText.text = currentWave.ToString();
        StartCoroutine(WaveStart());
    }

    private void WaveController_OnTimerUpdated(float? time)
    {
        countdownText.text = time != null ? $"{(int)time}s" : "0s";
    }

    private IEnumerator WaveEnd()
    {
        //Bounce badge into frame
        LeanTween.moveY(timer, 0, 1.5f).setEaseOutBounce(); ;
        yield return new WaitForSeconds(1.5f);
    }

    private IEnumerator WaveStart()
    {
        //Play new wave sound
        SfxPlayer.instance.Play(newWaveClip);
        //Expo timer out of scene
        LeanTween.moveY(timer, 200, 1).setEaseInExpo();
        yield return new WaitForSeconds(1);
        
        //Bounce badge into frame
        LeanTween.moveY(badge, -40, 1).setEaseOutBounce();
        yield return new WaitForSeconds(3);
        
        //Expo badge out of frame
        LeanTween.moveY(badge, 375, 1).setEaseOutExpo();
    }
}