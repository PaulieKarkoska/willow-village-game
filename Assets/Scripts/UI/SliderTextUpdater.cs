using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextUpdater : MonoBehaviour
{
    private Slider _slider;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private string textPrefix = string.Empty;
    [SerializeField]
    private string textPostfix = string.Empty;

    private void Start()
    {
        _slider = gameObject.GetComponent<Slider>();
        _slider.onValueChanged.AddListener(num =>
        {
            text.text = $"{textPrefix}{num}{textPostfix}";
        });
    }
}