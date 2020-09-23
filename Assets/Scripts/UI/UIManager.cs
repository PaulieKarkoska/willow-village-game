using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Text Elements")]
    [SerializeField]
    private TextMeshProUGUI goldCountText;

    #region Individual Text Updates

    public void UpdateGoldCountText(int count)
    {
        goldCountText.text = count.ToString("N0");
    }

    #endregion
}
