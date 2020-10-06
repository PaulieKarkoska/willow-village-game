using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI _interactionText;
    private Camera _camera;

    private Color32 _gray;

    void Start()
    {
        _gray = new Color32(173, 173, 173, 255);
        _camera = Camera.main;
        _interactionText = GameObject.FindGameObjectWithTag("GameUI").GetComponent<UIManager>().interactionText;
    }

    void Update()
    {
        if (Physics.SphereCast(_camera.ScreenPointToRay(Input.mousePosition), 0.1f, out RaycastHit hitInfo, 3)
            && hitInfo.transform.GetComponent<IInteractable>() != null)
        {
            var interactable = hitInfo.transform.GetComponent<IInteractable>();

            if (interactable.canInteract(this.gameObject))
            {
                _interactionText.text = interactable.interactionText;
                if (Input.GetKeyDown(interactable.interactionKey))
                {
                    _interactionText.color = Color.white;
                    interactable.interact(this.gameObject);
                }
            }
            else
            {
                _interactionText.color = _gray;
                _interactionText.text = interactable.interactionInvalidText;
            }
        }
        else
        {
            _interactionText.color = Color.white;
            _interactionText.text = string.Empty;
        }
    }
}