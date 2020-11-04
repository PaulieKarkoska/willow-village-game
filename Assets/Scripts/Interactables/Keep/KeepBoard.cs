using TMPro;
using UnityEngine;

public class KeepBoard : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TextMeshProUGUI waveText;
    [SerializeField]
    private TextMeshProUGUI routedText;
    [SerializeField]
    private TextMeshProUGUI aliveText;
    [SerializeField]
    private TextMeshProUGUI allyText;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI goldText;

    private WaveController wController;

    public bool supportsIntermediateInteraction => false;

    public KeyCode interactionKey => KeyCode.E;

    void Start()
    {
        wController = GameObject.Find("WaveController").GetComponent<WaveController>();

        KeepChest.OnGoldUpdated += KeepChest_OnGoldUpdated;
        AllySpawner.OnAllyKilled += AllySpawner_OnAllyKilled;
        AllySpawner.OnAllySpawned += AllySpawner_OnAllySpawned;

        EnemySpawner.OnEnemyKilled += EnemySpawner_OnEnemyKilled;
        EnemySpawner.OnEnemySpawned += EnemySpawner_OnEnemySpawned;
        EnemySpawner.OnAllEnemiesKilled += EnemySpawner_OnAllEnemiesKilled;

        WaveController.OnTimerUpdated += WaveController_TimerUpdated;
        WaveController.OnWaveUpdated += WaveController_OnWaveUpdated;
        WaveController.OnWaveStarted += WaveController_OnWaveStarted;

        waveText.text = $"0/{WaveController.maxWave}";
        routedText.text = "0/0";
        aliveText.text = "0";
        allyText.text = "0";
        timerText.text = "0s";
        goldText.text = "0";
    }


    private void KeepChest_OnGoldUpdated(string newGoldText)
    {
        goldText.text = newGoldText;
    }
    private void AllySpawner_OnAllyKilled(int alliesRemaining)
    {
        allyText.text = alliesRemaining.ToString();
    }
    private void AllySpawner_OnAllySpawned(int alliesRemaining)
    {
        allyText.text = alliesRemaining.ToString();
    }
    private void EnemySpawner_OnEnemyKilled(int routed, int total)
    {
        aliveText.text = EnemySpawner.concurrentCount.ToString();
        routedText.text = $"{EnemySpawner.routedEnemyCount}/{EnemySpawner.waveEnemyCount}";
    }
    private void EnemySpawner_OnEnemySpawned()
    {
        aliveText.text = EnemySpawner.concurrentCount.ToString();
    }
    private void EnemySpawner_OnAllEnemiesKilled()
    {
        aliveText.text = "0";
        routedText.text = "-/-";
    }
    private void WaveController_TimerUpdated(float? time)
    {
        if (time != null)
            timerText.text = $"{(int)time}s";
        else
            timerText.text = "~";
    }
    private void WaveController_OnWaveUpdated(int currentWave)
    {
        waveText.text = $"{wController.currentWave}/{WaveController.maxWave}";
    }
    private void WaveController_OnWaveStarted(int currentWave)
    {
        routedText.text = $"0/{EnemySpawner.WaveInfo.waveEnemyCount}";
    }

    public void focusLost(GameObject player)
    {
    }
    public string getInteractionText(GameObject player)
    {
        return wController.currentWave > 0 ? "Begin next wave" : $"Let the mayhem commence!";
    }
    public string getInteractionInvalidText(GameObject player)
    {
        return "Finish the current wave first!";
    }
    public bool canInteract(GameObject player)
    {
        return (wController.nextWaveTimeRemaining > 0
                && !wController.waveIsInProgress)
                || wController.currentWave == 0;
    }
    public void interact(GameObject player)
    {
        wController.StartNextWave();
    }
    public bool canIntermediateInteract(GameObject player)
    {
        return false;
    }
    public void intermediateInteract(GameObject player)
    {
    }
}