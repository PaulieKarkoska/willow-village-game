using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private float fadeOutDelay = 1;
    [SerializeField]
    private float fadeOutRate = 5;
    [SerializeField]
    private CanvasGroup cGroup;

    [SerializeField]
    private AudioClip completedAudio;

    [SerializeField]
    private Toggle[] toggles;
    [SerializeField]
    private TextMeshProUGUI[] texts;
    [SerializeField]
    private bool[] statuses;

    private bool allObjectivesCompleted
    {
        get
        {
            return statuses.All(s => s);
        }
    }
    private bool destroying = false;

    private Transform playerTransform;
    private WaveController wController;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        wController = GameObject.Find("WaveController").GetComponent<WaveController>();

        FirstSeedPickup.OnFirstSeedCollected += FirstSeedPickup_OnFirstSeedCollected;
        KeepChest.OnGoldUpdated += KeepChest_OnGoldUpdated;
        Well.OnWaterCollected += Well_OnWaterCollected;
    }

    private void KeepChest_OnGoldUpdated(string goldText)
    {
        ToggleChecked(3);
        KeepChest.OnGoldUpdated -= KeepChest_OnGoldUpdated;
    }

    private void Well_OnWaterCollected(GameObject well)
    {
        ToggleChecked(2);
        Well.OnWaterCollected -= Well_OnWaterCollected;
    }

    private void FirstSeedPickup_OnFirstSeedCollected()
    {
        ToggleChecked(1);
        FirstSeedPickup.OnFirstSeedCollected -= FirstSeedPickup_OnFirstSeedCollected;
    }

    private void Update()
    {
        if (!statuses[0]
            && (Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.D)))
        {
            ToggleChecked(0);
        }

        if (allObjectivesCompleted && !destroying)
            StartCoroutine(FadeOut());
    }

    private void ToggleChecked(int index)
    {
        toggles[index].isOn = true;
        texts[index].alpha = 0.6f;
        statuses[index] = true;
        AudioSource.PlayClipAtPoint(completedAudio, playerTransform.position);
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeOutDelay);
        while (cGroup.alpha > 0)
        {
            cGroup.alpha = Mathf.MoveTowards(cGroup.alpha, 0, Time.deltaTime / fadeOutRate);
            yield return null;
        }
        if (wController.currentWave == 0 && !wController.waveIsInProgress)
            wController.StartWaveCountdown(30);

        Destroy(gameObject, 1);
    }
}