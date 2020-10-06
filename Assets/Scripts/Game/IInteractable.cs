using UnityEngine;

public interface IInteractable
{
    string interactionText { get; }
    string interactionInvalidText { get; }

    void interact(GameObject obj);

    bool canInteract(GameObject obj);

    KeyCode interactionKey { get; }
}