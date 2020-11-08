using Invector.vCharacterController;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [Header("Victory")]
    [SerializeField]
    private RectTransform victoryPanel;
    [SerializeField]
    private Button victoryRestartButton;
    [SerializeField]
    private Button victoryQuitButton;
    [SerializeField]
    private AudioClip victoryClip;

    [Header("Defeat")]
    [SerializeField]
    private RectTransform defeatPanel;
    [SerializeField]
    private Button defeatRestartButton;
    [SerializeField]
    private Button defeatQuitButton;
    [SerializeField]
    private AudioClip defeatClip;

    private Vector2 cursorHotspot;

    [Header("Pause Menu")]
    [SerializeField]
    private GameObject pausePanel;

    private bool isPaused = false;
    private bool gameIsOver = false;

    private void Awake()
    {
        cursorHotspot = new Vector2(cursorSprite.width * 0.35f, cursorSprite.height * 0.2f);
        Cursor.SetCursor(null, cursorHotspot, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Start()
    {
        defeatRestartButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        defeatQuitButton.onClick.AddListener(() => SceneManager.LoadScene(0));

        victoryRestartButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        victoryQuitButton.onClick.AddListener(() => SceneManager.LoadScene(0));

        TreeOfLife.OnTreeKilled += TreeOfLife_OnTreeKilled;
        WaveController.OnLastWaveCompleted += WaveController_OnLastWaveCompleted;
    }

    private void WaveController_OnLastWaveCompleted()
    {
        gameIsOver = true;

        victoryPanel.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<vMeleeCombatInput>().enabled = false;
        LeanTween.moveY(victoryPanel, 250, 2).setEaseOutCirc();

        SfxPlayer.instance.Play(victoryClip);
        Cursor.SetCursor(cursorSprite, cursorHotspot, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void TreeOfLife_OnTreeKilled()
    {
        gameIsOver = true;

        defeatPanel.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<vMeleeCombatInput>().enabled = false;
        SfxPlayer.instance.Play(defeatClip);
        LeanTween.moveY(defeatPanel, 250, 2).setEaseOutExpo();

        Cursor.SetCursor(cursorSprite, cursorHotspot, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsOver)
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