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
 * Last Modified By: Abraar Sadek
 * Last Modified Date: 03/24/2025
 * Last Modified Made: Initial creation of the MainMenuManager script with 
 *                     functionality for play button, sound toggle, and exit button.
 */

//MainMenuManager - manages the main menu UI and simple menu actions
public class MainMenuManager : MonoBehaviour {

    [Header("Play Button References:")]
    public GameObject mainMenu; //Main menu panel GameObject reference
    public GameObject player; //Player GameObject reference (enabled when starting the game)
    public GameObject playerUI; //Player UI GameObject reference (enabled when starting the game)

    [Header("Sound Button References:")]
    public Button soundButton; //Reference to the UI Button used for sound on/off
    public Sprite targetGraphic; //Sprite shown when the button is in its default/target (off) state
    public Sprite selectedSprite; //Sprite shown when the button is selected/toggled on
    public AudioSource backgroundMusic; //AudioSource that plays background music (assign in Inspector)

    private bool isSoundOn; //Tracks whether background music is currently on
    

    //Start - Called once when script is first initialized
    private void Start() {
        //If-Statement - Derive initial sound state from AudioSource if available; default to true when absent
        if (backgroundMusic != null) {
            isSoundOn = backgroundMusic.isPlaying;
        } else {
            isSoundOn = true;
        } //End of If-Statement

        //If-Statement - Ensure the sound button starts with the correct sprite based on the current sound state
        if (soundButton != null && soundButton.image != null) {
            soundButton.image.sprite = isSoundOn ? (targetGraphic ?? soundButton.image.sprite) : (selectedSprite ?? soundButton.image.sprite);
        } //End of If-Statement

        //If-Statement - Ensure the AudioSource playback matches isSoundOn (play or pause)
        if (backgroundMusic != null) {

            if (isSoundOn) {
                if (!backgroundMusic.isPlaying) {
                    PlayerPrefs.SetInt("isSoundOn", 1);
                    backgroundMusic.Play(); 
                }
            } else {
                PlayerPrefs.SetInt("isSoundOn", 0);
                backgroundMusic.Pause(); 
            }

        } //End of If-Statement
        if (PlayerPrefs.GetInt("ShouldAutoPlay", 0) == 1)
        {
            Debug.Log("AutoPlay");
            PlayerPrefs.DeleteKey("ShouldAutoPlay");
            OnPlayButtonClick();
        }
    } //End of Start Method

    //OnPlayButtonClick - toggles main menu visibility and enables/disables the player
    public void OnPlayButtonClick() {

        //If-Statement - If the main menu is active, start the game (hide menu and enable player and player UI
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

    //OnSoundButtonClick - toggles background music on/off and updates the button sprite
    public void OnSoundButtonClick() {

        //If-Statement - Guard: Ensures the button and its image component exist (audio still toggled if AudioSource assigned)
        if (soundButton == null || soundButton.image == null) {
            Debug.LogWarning("Sound button or its Image component is not assigned.");
            // Do not return here; continue to toggle audio if backgroundMusic exists
        } //End of If-Statement

        //Flip state - Toggle sound on/off
        isSoundOn = !isSoundOn;

        //If-Statement - Update the button sprite to reflect the new sound state
        if (soundButton != null && soundButton.image != null) {

            if (isSoundOn) {
            
                if (targetGraphic != null) {
                    soundButton.image.sprite = targetGraphic;
                } else {
                    Debug.LogWarning("Target graphic is not assigned.");
                } //End of If-Else Statement
            
            } else {

                if (selectedSprite != null) {
                    soundButton.image.sprite = selectedSprite;
                } else {
                    Debug.LogWarning("Selected sprite is not assigned.");
                } //End of If-Else Statement
            
            } //End of If-Else Statement

        } //End of If-Statement

        //If-Statement - Toggle the background music playback (preserve playback position with Pause/UnPause)
        if (backgroundMusic != null) {

            if (isSoundOn) {

                backgroundMusic.UnPause();
                
                if (!backgroundMusic.isPlaying) { backgroundMusic.Play(); }

                Debug.Log("Background music turned ON.");
            
            } else {

                backgroundMusic.Pause();
                Debug.Log("Background music turned OFF.");
            
            } //End of If-Else Statement
        
        } else {
            Debug.Log("No backgroundMusic AudioSource assigned — only sprite toggled.");
        } //End of If-Statement

        Debug.Log("Sound button clicked. Toggling sound to: " + (isSoundOn ? "ON" : "OFF"));

    } //End of OnSoundButtonClick Method

    //OnExitButtonClick - quits the application
    public void OnExitButtonClick() {

        Debug.Log("Exit button clicked. Quitting application...");
        Application.Quit();

    } //End of OnExitButtonClick Method

} //End of MainMenuManager Class
