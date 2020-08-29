using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject menuPanel;
    public GameObject newGamePanel;
    public GameObject settingsPanel;

    [Header("Menu Buttons")]
    public Button NewGameButton;
    public Button ContinueButton;
    public Button SettingsButton;
    public Button ExitButton;

    [Header("New Game Buttons")]
    public Button NewGame_YesButton;
    public Button NewGame_NoButton;

    private void Start()
    {
        newGamePanel.SetActive(false);
        settingsPanel.SetActive(false);

        //if (SaveManager.HasSave)
        // Disable Continue Button
    }

    public void NewGame()
    {
        //if (SavaManager.HasSave)
        if (true)
        {
            TogglePanel(newGamePanel, true);
        }
        else
        {
            //SaveManager.ClearCurrentSave();
            SceneManager.LoadScene(1);
        }
    }

    public void NewGameYes()
    {
        SceneManager.LoadScene(1);
    }

    public void NewGameNo()
    {
        TogglePanel(newGamePanel, false);
    }

    public void Continue()
    {
        Debug.Log("continue pressed");

    }

    public void Settings()
    {
        Debug.Log("settings pressed");

    }

    public void Exit()
    {
        Debug.Log("exit pressed");

    }

    private void TogglePanel(GameObject panel, bool active)
    {
        panel.SetActive(active);
        NewGameButton.interactable = !active;
        ContinueButton.interactable = !active;
        SettingsButton.interactable = !active;
        ExitButton.interactable = !active;
    }
}
