using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {

    [Header("Panels")]
    public GameObject pauseMenuPanel; // Reference to the pause menu UI panel

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

    } //End of Start Method

    // Update is called once per frame - Poll for pause/unpause input each frame.
    void Update () {
        ActivatePauseMenuPanel();
    } //End of Update Method

    // Toggle the pause menu when the pause key is pressed.
    public void ActivatePauseMenuPanel () {

        //Ensure we have a panel reference before attempting to toggle it.
        if (pauseMenuPanel == null) {
            return;
        } //End of If-Statement

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {

            Debug.Log("Pause button pressed. Toggling pause menu...");

            if (pauseMenuPanel.activeInHierarchy == true) {
                // If the pause menu is active, hide it and resume time.
                pauseMenuPanel.SetActive(false);
                Time.timeScale = 1f; // Resume the game
            } else {
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
