using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public static WaveController instance { get; private set; }

    public int currentWave { get; private set; } = 0;
    public const int maxWave = 14;

    public float nextWaveTimeRemaining { get; private set; } = 0;
    public bool timerIsCountingDown { get; private set; } = false;
    public bool waveIsInProgress { get; set; } = false;

    public Dictionary<int, WaveInfo> Waves { get; private set; }

    public delegate void LastWaveCompleted();
    public static event LastWaveCompleted OnLastWaveCompleted;

    public delegate void TimerUpdated(float? time);
    public static event TimerUpdated OnTimerUpdated;

    public delegate void WaveUpdated(int currentWave);
    public static event WaveUpdated OnWaveUpdated;

    public delegate void CountdownStarted();
    public static event CountdownStarted OnCountdownStarted;

    public delegate void WaveStarted(int currentWave);
    public static event WaveStarted OnWaveStarted;

    private EnemySpawner eSpawn;
    private AllySpawner aSpawn;

    private void Start()
    {
        instance = this;
        eSpawn = GameObject.Find("BanditSpawner").GetComponent<EnemySpawner>();
        aSpawn = GameObject.Find("SoldierSpawner").GetComponent<AllySpawner>();

        var xmlText = Resources.Load("WaveInfo") as TextAsset;
        var xdoc = XDocument.Parse(xmlText.text);
        var waveList = xdoc.XPathSelectElements("/root/Waves/*").Select(x =>
            new WaveInfo(int.Parse(x.Attribute("number").Value),
                         int.Parse(x.Element("enemiesPerWave").Value),
                         int.Parse(x.Element("waitTimeAfter").Value),
                         bool.Parse(x.Element("enemiesHaveWeapon").Value),
                         bool.Parse(x.Element("enemiesHaveShield").Value)));
        Waves = waveList.ToDictionary(w => w.number, w => w);
        OnTimerUpdated?.Invoke(null);
    }

    private void Update()
    {
        if (timerIsCountingDown)
        {
            if (timerIsCountingDown && nextWaveTimeRemaining > 0)
            {
                nextWaveTimeRemaining -= Time.deltaTime;
                OnTimerUpdated?.Invoke(nextWaveTimeRemaining);
            }
            else
            {
                StartNextWave();
                OnTimerUpdated?.Invoke(null);
                timerIsCountingDown = false;
            }
        }
    }

    public void StartNextWave()
    {
        timerIsCountingDown = false;
        OnTimerUpdated?.Invoke(null);

        currentWave++;
        OnWaveUpdated?.Invoke(currentWave);

        EnemySpawner.WaveInfo = Waves[currentWave];
        nextWaveTimeRemaining = EnemySpawner.WaveInfo.waitTimeAfter;

        OnWaveStarted?.Invoke(currentWave);

        eSpawn.StartSpawning();
        if (currentWave == 1)
            aSpawn.StartSpawning();
    }

    public void StartWaveCountdown(float? countdownOverride = null)
    {
        if (countdownOverride != null)
            nextWaveTimeRemaining = (float)countdownOverride;

        if (currentWave <= maxWave)
        {
            timerIsCountingDown = true;
            OnCountdownStarted?.Invoke();
        }
        else
        {
            OnLastWaveCompleted?.Invoke();
        }
    }
}

public class WaveInfo
{
    public WaveInfo(int num, int enemies, float subsequentRoundDelay, bool weapon, bool shield)
    {
        number = num;
        waveEnemyCount = enemies;
        waitTimeAfter = subsequentRoundDelay;
        enemiesHaveWeapon = weapon;
        enemiesHaveShield = shield;
    }

    public WaveInfo() { }

    public bool enemiesHaveWeapon { get; set; }
    public bool enemiesHaveShield { get; set; }
    public int number { get; set; }
    public int waveEnemyCount { get; set; }
    public float waitTimeAfter { get; set; }
}