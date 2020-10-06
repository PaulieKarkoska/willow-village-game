using UnityEngine;

public class Well : MonoBehaviour, IInteractable
{
    [Header("Farming")]
    [SerializeField]
    private GameObject _water;
    [SerializeField]
    private AudioClip[] _waterAudioClips;

    public string interactionText { get; } = "Refill water level";

    public string interactionInvalidText { get; } = "Water level is already full";

    public KeyCode interactionKey { get; } = KeyCode.E;

    public bool canInteract(GameObject obj)
    {
        return obj.CompareTag("Player") && obj.GetComponent<CollectableManager>() && obj.GetComponent<CollectableManager>().waterLevel < 5;
    }

    public void interact(GameObject obj)
    {
        obj.GetComponent<CollectableManager>().RefillWaterLevel();

        AudioSource.PlayClipAtPoint(_waterAudioClips[Random.Range(0, _waterAudioClips.Length)], transform.position);
    }
}
