using Invector.vCharacterController;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawner : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private vThirdPersonController playerController;

    [Header("Respawning")]
    [SerializeField]
    private Transform respawnSpot;
    [SerializeField]
    private float respawnDelay;
    [SerializeField]
    private float respawnDelayAdder;
    [SerializeField]
    private float maxRespawnDelay;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private Image reticle;

    private void Start()
    {
        playerController.onDead.AddListener((g) => StartCoroutine(RespawnCountdown()));
    }

    private IEnumerator RespawnCountdown()
    {
        yield return new WaitForSeconds(3);
        player.transform.position = respawnSpot.position;
        player.transform.rotation = respawnSpot.rotation;

        timerText.text = $"Respawning in {respawnDelay} seconds";
        SetUI(false);

        float secondsLeft = respawnDelay;

        while (secondsLeft > 0)
        {
            secondsLeft -= Time.deltaTime;
            timerText.text = $"Respawning in {secondsLeft:0} seconds";
            yield return null;
        }
        playerController.ResetHealth();

        if (respawnDelay < maxRespawnDelay)
            respawnDelay += respawnDelayAdder;

        SetUI(true);
    }

    private void SetUI(bool isRespawning)
    {
        var retColor = reticle.color;
        retColor.a = isRespawning ? 1f : 0f;
        reticle.color = retColor;

        timerText.enabled = !isRespawning;
    }
}