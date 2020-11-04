using TMPro;
using UnityEngine;

public class KeepBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI waveText;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private TextMeshProUGUI enemyText;
    [SerializeField]
    private TextMeshProUGUI allyText;

    void Start()
    {
        KeepChest.OnGoldUpdated += KeepChest_OnGoldUpdated;
        AllySpawner.OnAllyKilled += AllySpawner_OnAllyKilled;
        AllySpawner.OnAllySpawned += AllySpawner_OnAllySpawned;

        EnemySpawner.OnEnemyKilled += EnemySpawner_OnEnemyKilled;
        EnemySpawner.OnEnemySpawned += EnemySpawner_OnEnemySpawned;
        EnemySpawner.OnAllEnemiesKilled += EnemySpawner_OnAllEnemiesKilled;

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
}