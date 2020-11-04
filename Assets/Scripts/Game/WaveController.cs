using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public int currentWave { get; private set; } = 0;
    public const int maxWave = 15;

    public float nextWaveTimeRemaining { get; private set; } = 0;
    public bool timerIsCountingDown { get; private set; } = false;

    public Dictionary<int, WaveInfo> Waves { get; private set; }

    public delegate void TimerUpdated(float? time);
    public static event TimerUpdated OnTimerUpdated;

    public delegate void WaveUpdated(int currentWave);
    public static event WaveUpdated OnWaveUpdated;

    private void Start()
    {
        var xmlText = Resources.Load("WaveInfo") as TextAsset;
        var xdoc = XDocument.Parse(xmlText.text);
        var waveList = xdoc.XPathSelectElements("/root/Waves/*").Select(x =>
            new WaveInfo(int.Parse(x.Attribute("number").Value),
                         int.Parse(x.Element("enemiesPerWave").Value),
                         int.Parse(x.Element("waitTimeAfter").Value)));
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
    }

    public void StartWaveCountdown()
    {
        timerIsCountingDown = true;
    }
}

public class WaveInfo
{
    public WaveInfo(int num, int enemies, float subsequentRoundDelay)
    {
        number = num;
        waveEnemyCount = enemies;
        waitTimeAfter = subsequentRoundDelay;
    }

    public WaveInfo() { }

    public int number { get; set; }
    public int waveEnemyCount { get; set; }
    public float waitTimeAfter { get; set; }
}