using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Text Elements")]
    [SerializeField]
    private TextMeshProUGUI goldCountText;
    [SerializeField]
    private TextMeshProUGUI seedCountText;

    #region Individual Text Updates

    public void UpdateGoldCountText(int count)
    {
        goldCountText.text = count.ToString("N0");
    }

    public void UpdateSeedCountText(int count)
    {
        seedCountText.text = count.ToString("N0");    }

    #endregion
}
