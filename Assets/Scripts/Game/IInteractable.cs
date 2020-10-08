using UnityEngine;

public interface IInteractable
{
    void focusLost(GameObject obj);

    string getInteractionText(GameObject obj);
    string getInteractionInvalidText(GameObject obj);

    bool supportsIntermediateInteraction { get; }

    bool canInteract(GameObject obj);
    void interact(GameObject obj);

    bool canIntermediateInteract(GameObject obj);
    void intermediateInteract(GameObject obj);

    KeyCode interactionKey { get; }
}