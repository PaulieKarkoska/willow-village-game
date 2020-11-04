using TMPro;
using UnityEngine;

public class KeepBoard : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TextMeshProUGUI waveText;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private TextMeshProUGUI enemyText;
    [SerializeField]
    private TextMeshProUGUI allyText;
    [SerializeField]
    private TextMeshProUGUI timerText;

    private WaveController waveController;

    public bool supportsIntermediateInteraction => false;

    public KeyCode interactionKey => KeyCode.E;

    void Start()
    {
        waveController = GameObject.Find("WaveController").GetComponent<WaveController>();

        KeepChest.OnGoldUpdated += KeepChest_OnGoldUpdated;
        AllySpawner.OnAllyKilled += AllySpawner_OnAllyKilled;
        AllySpawner.OnAllySpawned += AllySpawner_OnAllySpawned;

        EnemySpawner.OnEnemyKilled += EnemySpawner_OnEnemyKilled;
        EnemySpawner.OnEnemySpawned += EnemySpawner_OnEnemySpawned;
        EnemySpawner.OnAllEnemiesKilled += EnemySpawner_OnAllEnemiesKilled;
        
        WaveController.OnTimerUpdated += WaveController_TimerUpdated;
        WaveController.OnWaveUpdated += WaveController_OnWaveUpdated;

        goldText.text = "0";
        enemyText.text = "0/0";
        allyText.text = "0";
        waveText.text = "0/20";
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

        enemyText.text = $"{EnemySpawner.routedEnemyCount}/{EnemySpawner.waveEnemyCount}";
    }
    private void EnemySpawner_OnEnemySpawned()
    {
    }
    private void EnemySpawner_OnAllEnemiesKilled()
    {
        enemyText.text = "-/-";
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
        waveText.text = $"{waveController.currentWave}/{WaveController.maxWave}";
    }

    public void focusLost(GameObject player)
    {
    }
    public string getInteractionText(GameObject player)
    {
        return waveController.currentWave > 0 ? "Begin next wave" : $"Let the mayhem commence!";
    }
    public string getInteractionInvalidText(GameObject player)
    {
        return "Finish the current wave first!";
    }
    public bool canInteract(GameObject player)
    {
        return waveController.nextWaveTimeRemaining > 0 || waveController.currentWave == 0;
    }
    public void interact(GameObject player)
    {
        waveController.StartNextWave();
    }
    public bool canIntermediateInteract(GameObject player)
    {
        return false;
    }
    public void intermediateInteract(GameObject player)
    {
    }
}