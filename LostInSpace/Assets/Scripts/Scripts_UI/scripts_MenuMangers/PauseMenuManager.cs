using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {

    [Header("Panels")]
    public GameObject pauseMenuPanel; // Reference to the pause menu UI panel
    public GameObject mainMenuPanel; // Reference to the main menu UI panel - used to prevent pausing while main menu is shown
    public GameObject gameOverPanel; // Reference to the game over UI panel - used to prevent pausing while game over is shown

    [Header("Audio References:")]
    public Button soundButton; // Button used to toggle sound via SoundManager

    // Start is called before the first frame update
    // Register the sound button with the SoundManager if available.
    private void Start() {

        if (SoundManager.Instance != null && soundButton != null) {
            SoundManager.Instance.RegisterButton(soundButton);
        } //End of If-Statement

        if (pauseMenuPanel == null) {
            Debug.LogWarning("PauseMenuManager: pauseMenuPanel is not assigned in the inspector.");
        } //End of If-Statement

        if (mainMenuPanel == null) {
            Debug.LogWarning("PauseMenuManager: mainMenuPanel is not assigned in the inspector.");
        } //End of If-Statement

        if (gameOverPanel == null) {
            Debug.LogWarning("PauseMenuManager: gameOverPanel is not assigned in the inspector.");
        } //End of If-Statement

    } //End of Start Method

    // Update is called once per frame - Poll for pause/unpause input each frame.
    void Update () {
        ActivatePauseMenuPanel();
    } //End of Update Method

    // Toggle the pause menu when the pause key is pressed.
    public void ActivatePauseMenuPanel () {

        // Ensure we have a panel reference before attempting to toggle it.
        if (pauseMenuPanel == null) {
            return;
        } //End of If-Statement

        // Check for pause/unpause key press.
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {

            Debug.Log("Pause button pressed. Toggling pause menu...");

            // If the pause menu is already visible, always allow closing it.
            if (pauseMenuPanel.activeInHierarchy == true) {
                // If the pause menu is active, hide it and resume time.
                pauseMenuPanel.SetActive(false);
                Time.timeScale = 1f; // Resume the game
            } else {
                // Before opening the pause menu, prevent activation if the main menu or game over panels are active.
                // This stops pause from being opened while in main menu or during game over.
                if ((mainMenuPanel != null && mainMenuPanel.activeInHierarchy == true)
                    || (gameOverPanel != null && gameOverPanel.activeInHierarchy == true)) {

                    Debug.Log("PauseMenuManager: Pause menu will not open because Main Menu or Game Over panel is active.");
                    return;
                } //End of If-Statement

                // If the pause menu is inactive, show it and pause time.
                pauseMenuPanel.SetActive(true);
                Time.timeScale = 0f; // Pause the game
            } //End of If-Statement

        } //End of If-Statement

    } //End of ActivatePauseMenuPanel Method

    // Called from UI to return to the current scene (acts as "home" here).
    public void OnHomeButtonClick() {

        Debug.Log("Home button clicked. Heading Home");
        //Reload the current scene (acts as returning to home/starting point).
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // Resume the game before changing scenes

    } //End of OnHomeButtonClick Method

    // Called from UI to unpause the game.
    public void OnUnpauseButtonClick() {

        Debug.Log("Unpause button clicked. Unpausing Game");

        if (pauseMenuPanel != null) {
            pauseMenuPanel.SetActive(false);
        } //End of If-Statement

        Time.timeScale = 1f; // Resume the game

    } //End of OnUnpauseButtonClick Method

    // Called from UI to toggle sound via the project's SoundManager.
    public void OnSoundButtonClick() {

        if (SoundManager.Instance != null) {
            SoundManager.Instance.ToggleSound();
        } //End of If-Statement

    } //End of OnSoundButtonClick Method

} //End of PauseMenuManager Class
