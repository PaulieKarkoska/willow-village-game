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
    public GameObject settingsPanel;
    [Header("Loading")]
    public GameObject loadingPanel;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;

    [Header("Menu Buttons")]
    public Button NewGameButton;
    public Button SettingsButton;
    public Button ExitButton;

    private AsyncOperation operation;

    private void Start()
    {
        newGamePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void NewGame()
    {
        interactionPanel.SetActive(false);
        StartCoroutine(BeginLoad(1));
    }
    private IEnumerator BeginLoad(int sceneIndex)
    {
        loadingPanel.SetActive(true);
        operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            loadingSlider.value = operation.progress;
            loadingText.text = $"Loading... {(int)(operation.progress * 100f) + 10}%";
            yield return null;
        }
    }

    public void Settings()
    {
        TogglePanel(settingsPanel, true);

    }
    public void SettingsClose()
    {
        TogglePanel(settingsPanel, false);
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