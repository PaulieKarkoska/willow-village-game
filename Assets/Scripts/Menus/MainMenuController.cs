using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject interactionPanel;
    [Space(10)]
    public GameObject menuPanel;
    public GameObject newGamePanel;
    public GameObject controlsPanel;
    [Header("Loading")]
    public GameObject loadingPanel;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;

    [Header("Menu Buttons")]
    public Button NewGameButton;
    public Button SettingsButton;
    public Button ExitButton;

    [Header("Cursor")]
    public Texture2D cursorSprite;

    private AsyncOperation operation;

    private void Start()
    {
        Time.timeScale = 1;

        Cursor.SetCursor(cursorSprite, new Vector2(cursorSprite.width * 0.35f, cursorSprite.height * 0.2f), CursorMode.ForceSoftware);
        newGamePanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    public void NewGame()
    {
        interactionPanel.SetActive(false);
        StartCoroutine(BeginLoad(1));
    }
    private IEnumerator BeginLoad(int sceneIndex)
    {
        Camera.main.GetComponent<Animator>().Play("MainMenuCameraAnim");

        loadingPanel.SetActive(true);
        operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            loadingSlider.value = operation.progress;
            loadingText.text = $"Loading... {((int)(operation.progress * 100f)) + 10}%";
            yield return null;
        }
    }

    public void Settings()
    {
        TogglePanel(controlsPanel, true);

    }
    public void SettingsClose()
    {
        TogglePanel(controlsPanel, false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void TogglePanel(GameObject panel, bool active)
    {
        panel.SetActive(active);
        NewGameButton.interactable = !active;
        SettingsButton.interactable = !active;
        ExitButton.interactable = !active;
    }

}