using System.Collections;
using TMPro;
using UnityEngine;

public class WaveBadge : MonoBehaviour
{
    private WaveController wController;

    [SerializeField]
    private TextMeshProUGUI waveNumberText;
    [SerializeField]
    private TextMeshProUGUI countdownText;


    void Start()
    {
        wController = GameObject.Find("WaveController").GetComponent<WaveController>();
    }

    void Update()
    {
        WaveController.OnTimerUpdated += WaveController_OnTimerUpdated;
        WaveController.OnWaveStarted += WaveController_OnWaveStarted;
    }

    private void WaveController_OnWaveStarted(int currentWave)
    {
        waveNumberText.text = currentWave.ToString();
    }

    private void WaveController_OnTimerUpdated(float? time)
    {
        if (time != null)
        {
            countdownText.text = $"{(int)time}s";
            StartCoroutine(SweepInTimer());
        }
        else
        {
            countdownText.text = "~";
            StartCoroutine(SweepOutTimer());
        }
    }


    private IEnumerator SweepInTimer()
    {
        yield return null;
    }
    private IEnumerator SweepOutTimer()
    {
        yield return null;
    }


    private IEnumerator SweepInOutRound()
    {
        yield return null;
    }
}