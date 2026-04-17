using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/*
 * Created By: Abraar Sadek
 * Created Date: N/A
 * Purpose: Responsible for the Pause Menu
 * 
 * Last Modified By: Drew Oro
 * Last Modified Date: 04/07/2026
 * Last Modified Made: Removing Comments
 */

public class PauseMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pauseMenuPanel;
    public GameObject mainMenuPanel;
    public GameObject gameOverPanel;
    [Header("Audio References:")]
    public Button soundButton;
    [Header("Pause Reference")]
    public bool isPause;
    private void Start()
    {
        if (SoundManager.Instance != null && soundButton != null)
        {
            SoundManager.Instance.RegisterButton(soundButton);
        }
        if (pauseMenuPanel == null)
        {
            Debug.LogWarning("PauseMenuManager: pauseMenuPanel is not assigned in the inspector.");
        }
        if (mainMenuPanel == null)
        {
            Debug.LogWarning("PauseMenuManager: mainMenuPanel is not assigned in the inspector.");
        }
        if (gameOverPanel == null)
        {
            Debug.LogWarning("PauseMenuManager: gameOverPanel is not assigned in the inspector.");
        }
        isPause = false;
    }
    void Update()
    {
        ActivatePauseMenuPanel();
    }
    public void ActivatePauseMenuPanel()
    {
        if (pauseMenuPanel == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pause button pressed. Toggling pause menu...");
            if (pauseMenuPanel.activeInHierarchy == true)
            {
                pauseMenuPanel.SetActive(false);
                Time.timeScale = 1f;
                isPause = false;
            }
            else
            {
                if ((mainMenuPanel != null && mainMenuPanel.activeInHierarchy == true)
                    || (gameOverPanel != null && gameOverPanel.activeInHierarchy == true))
                {
                    Debug.Log("PauseMenuManager: Pause menu will not open because Main Menu or Game Over panel is active.");
                    return;
                }
                pauseMenuPanel.SetActive(true);
                Time.timeScale = 0f;
                isPause = true;
            }
        }
    }
    public void OnHomeButtonClick()
    {
        Debug.Log("Home button clicked. Heading Home");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        isPause = false;
    }
    public void OnUnpauseButtonClick()
    {
        Debug.Log("Unpause button clicked. Unpausing Game");
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
        Time.timeScale = 1f;
        isPause = false;
    }
    public void OnSoundButtonClick()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ToggleSound();
        }
    }
}
