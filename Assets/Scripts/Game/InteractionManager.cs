using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public TextMeshProUGUI interactionText { get; private set; }
    public Camera interactionCamera { get; private set; }
    public Vector3 lookPoint { get; private set; }

    private Color32 _gray;
    private IInteractable _lastInteractable;

    void Start()
    {
        _gray = new Color32(180, 180, 180, 255);
        interactionCamera = Camera.main;
        interactionText = GameObject.FindGameObjectWithTag("GameUI").GetComponent<UIManager>().interactionText;
    }

    void Update()
    {
        IInteractable interactable = null;
        var ray = interactionCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 4, Color.green, 0.1f, true);

        if (Physics.Raycast(ray.origin, ray.direction * 4, out RaycastHit hitInfo, 4))
        {
            interactable = hitInfo.transform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (interactable != _lastInteractable)
                {
                    _lastInteractable?.focusLost(this.gameObject);
                    _lastInteractable = null;
                }

                _lastInteractable = interactable;

                lookPoint = hitInfo.point;

                if (interactable.supportsIntermediateInteraction && interactable.canIntermediateInteract(this.gameObject))
                {
                    interactable.intermediateInteract(this.gameObject);
                }

                if (interactable.canInteract(this.gameObject))
                {
                    interactionText.color = Color.white;
                    interactionText.text = interactable.getInteractionText(this.gameObject);
                    if (Input.GetKeyDown(interactable.interactionKey))
                    {
                        interactionText.color = Color.white;
                        interactable.interact(this.gameObject);
                    }
                }
                else
                {
                    interactionText.color = _gray;
                    interactionText.text = interactable.getInteractionInvalidText(this.gameObject);
                }
            }
            else
            {
                interactionText.color = Color.white;
                interactionText.text = string.Empty;
            }
        }
        else
        {
            interactionText.color = Color.white;
            interactionText.text = string.Empty;
        }

        if (_lastInteractable != null && interactable != _lastInteractable)
        {
            _lastInteractable.focusLost(this.gameObject);
            _lastInteractable = null;
        }
    }
}