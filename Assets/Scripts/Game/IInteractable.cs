using UnityEngine;

public interface IInteractable
{
    void focusLost(GameObject player);

    string getInteractionText(GameObject player);
    string getInteractionInvalidText(GameObject player);

    bool supportsIntermediateInteraction { get; }

    bool canInteract(GameObject player);
    void interact(GameObject player);

    bool canIntermediateInteract(GameObject player);
    void intermediateInteract(GameObject player);

    KeyCode interactionKey { get; }
}