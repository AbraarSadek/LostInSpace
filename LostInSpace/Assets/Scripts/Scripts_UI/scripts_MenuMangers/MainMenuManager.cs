using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Created By: Abraar Sadek
 * Created Date: 03/24/2025
 * Purpose: The purpose of this scripts is to manage the main menu UI and handle simple menu actions such as:
 *          starting the game, toggling sound, and exiting the application.
 * 
 * Last Modified By: Drew Oro
 * Last Modified Date: 03/31/2026
 * Last Modified Made: Connection to the Sound Manager, Added AutoPlay upon Replay, removed the specific comments.
 */

//MainMenuManager - manages the main menu UI and simple menu actions
public class MainMenuManager : MonoBehaviour {

    [Header("Play Button References:")]
    public GameObject mainMenu;
    public GameObject player;
    public GameObject playerUI;

    [Header("Sound Button References:")]
    public Button soundButton;

    //Start - Called once when script is first initialized
    private void Start() {

        if (SoundManager.Instance != null && soundButton != null) {
            SoundManager.Instance.RegisterButton(soundButton);
        }

        if (PlayerPrefs.GetInt("ShouldAutoPlay", 0) == 1) {
            Debug.Log("AutoPlay");
            PlayerPrefs.DeleteKey("ShouldAutoPlay");
            OnPlayButtonClick();
        }

    } //End of Start Method


    //OnPlayButtonClick - toggles main menu visibility and enables/disables the player
    public void OnPlayButtonClick() {

        //If-Statement - If the main menu is active, start the game (hide menu and enable player and player UI)
        if (mainMenu.activeInHierarchy == true) {

            Debug.Log("Play button clicked. Starting game...");
            mainMenu.SetActive(false);
            player.SetActive(true);
            playerUI.SetActive(true);

          //Otherwise show the main menu and disable the player and player UI.
        } else if (mainMenu.activeInHierarchy == false) {

            mainMenu.SetActive(true);
            player.SetActive(false);
            playerUI.SetActive(false);

        } //End of If-Statement

    } //End of OnPlayButtonClick Method

    public void OnSoundButtonClick() {
        if (SoundManager.Instance != null) {
            Debug.Log("Toggle Sound from MainMenu");
            SoundManager.Instance.ToggleSound();
        }
    }

    //OnExitButtonClick - quits the application
    public void OnExitButtonClick() {

        Debug.Log("Exit button clicked. Quitting application...");
        Application.Quit();

    } //End of OnExitButtonClick Method

} //End of MainMenuManager Class
