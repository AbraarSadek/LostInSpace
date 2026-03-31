using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Created By: Drew Oro
 * Created Date: 03/31/2026
 * Purpose: Manage the Game Over Screen
 * 
 * Last Modified By: Drew Oro
 * Last Modified Date: 03/31/2025
 * Last Modified Made: Copied from the MainMenu Manager, and applied the buttons
 */

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject mainMenuPanel;

    [Header("Sound Button References:")]
    public Button soundButton;

    private void Start()
    {
        if (SoundManager.Instance != null && soundButton != null) {
            SoundManager.Instance.RegisterButton(soundButton);
        }
    }


    public void activateGameOverPanel(bool isActive)
    {
        gameOverPanel.SetActive(isActive);
        if (gameOverPanel.activeInHierarchy == true) {
            Debug.Log("Activate GameOver Panel");
            gameOverPanel.SetActive(true);
        }
        else
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void OnSoundButtonClick()
    {
        if (SoundManager.Instance != null) {
            SoundManager.Instance.ToggleSound();
        }
    }
    public void OnHomeButtonClick()
    {
        Debug.Log("Home button clicked. Heading Home");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnRestartButtonClick()
    {
        Debug.Log("Restart Button Clicked");
        PlayerPrefs.SetInt("ShouldAutoPlay", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
