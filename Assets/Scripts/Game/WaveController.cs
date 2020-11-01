using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public int currentWave = 0;
    public const int maxWave = 20;

    public Dictionary<int, WaveInfo> Waves { get; private set; }

    private void Start()
    {
        var xmlText = Resources.Load("WaveInfo") as TextAsset;
        var xdoc = XDocument.Parse(xmlText.text);
        var waveList = xdoc.XPathSelectElements("/root/Waves/*").Select(x =>
            new WaveInfo(int.Parse(x.Attribute("number").Value),
                         int.Parse(x.Element("enemiesPerWave").Value),
                         int.Parse(x.Element("waitTimeAfter").Value)));
        Waves = waveList.ToDictionary(w => w.number, w => w);
    }
    public void StartFirstWave()
    {

    }

    public void ForceNextWave()
    {

    }
}

public class WaveInfo
{
    public WaveInfo(int num, int enemies, float subsequentRoundDelay)
    {
        number = num;
        enemiesPerWave = enemies;
        waitTimeAfter = subsequentRoundDelay;
    }

    public WaveInfo() { }

    public int number { get; set; }
    public int enemiesPerWave { get; set; }
    public float waitTimeAfter { get; set; }
}