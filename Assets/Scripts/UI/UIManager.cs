using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Cursor")]
    [SerializeField]
    private Texture2D cursorSprite;

    [Header("Text Elements")]
    [SerializeField]
    private TextMeshProUGUI goldCountText;
    [SerializeField]
    private TextMeshProUGUI seedCountText;
    [SerializeField]
    private TextMeshProUGUI waterLevelText;
    public TextMeshProUGUI interactionText;

    private Vector2 cursorHotspot;

    [Header("Pause Menu")]
    [SerializeField]
    private GameObject pausePanel;

    private bool isPaused = false;

    private void Awake()
    {
        cursorHotspot = new Vector2(cursorSprite.width * 0.35f, cursorSprite.height * 0.2f);
        Cursor.SetCursor(null, cursorHotspot, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    #region Individual Text Updates
    public void UpdateGoldCountText(int count)
    {
        goldCountText.text = count.ToString("N0");
    }

    public void UpdateSeedCountText(int count)
    {
        seedCountText.text = count.ToString("N0");
    }

    public void UpdateWaterLevelText(int level, int maxLevel)
    {
        waterLevelText.text = $"{level}/{maxLevel}";
    }
    #endregion

    #region Pause Menu
    private void TogglePauseMenu()
    {
        if (isPaused)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            pausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            Time.timeScale = 1;
        }
        else
        {
            Cursor.SetCursor(cursorSprite, cursorHotspot, CursorMode.ForceSoftware);
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            
            Time.timeScale = 0;
        }
        isPaused = !isPaused;
    }
    #endregion
}
