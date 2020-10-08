using UnityEngine;

public class Well : MonoBehaviour, IInteractable
{
    [Header("Farming")]
    [SerializeField]
    private GameObject _water;
    [SerializeField]
    private AudioClip[] _waterAudioClips;

    public bool supportsIntermediateInteraction { get; } = false;

    public string getInteractionText(GameObject player = null)
    {
        return "Refill water level";
    }
    public string getInteractionInvalidText(GameObject player = null)
    {
        return "Water level is already full";
    }

    public KeyCode interactionKey { get; } = KeyCode.E;

    public bool canInteract(GameObject player)
    {
        return player.CompareTag("Player") && player.GetComponent<CollectableManager>() && player.GetComponent<CollectableManager>().waterLevel < 5;
    }
    public void interact(GameObject player)
    {
        player.GetComponent<CollectableManager>().RefillWaterLevel();

        AudioSource.PlayClipAtPoint(_waterAudioClips[Random.Range(0, _waterAudioClips.Length)], transform.position);
    }

    public bool canIntermediateInteract(GameObject player)
    {
        return false;
    }
    public void intermediateInteract(GameObject player)
    {
    }

    public void focusLost(GameObject obj)
    {
    }
}
