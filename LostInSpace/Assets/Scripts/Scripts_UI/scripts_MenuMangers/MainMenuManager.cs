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
    [Header("Sound Effects")]
    public List<AudioSource> audioSoundEffects = new List<AudioSource>();
    private void Start() {


        
        if (SoundManager.Instance != null && soundButton != null) {
            SoundManager.Instance.RegisterButton(soundButton);
        }
        
        if (PlayerPrefs.GetInt("ShouldAutoPlay", 0) == 1) {
            Debug.Log("AutoPlay");
            PlayerPrefs.DeleteKey("ShouldAutoPlay");
            OnPlayButtonClick();
        }
        SoundManager.Instance.ApplySound();


    }


    public void OnPlayButtonClick() {
        if (mainMenu.activeInHierarchy == true) {

            Debug.Log("Play button clicked. Starting game...");
            mainMenu.SetActive(false);
            player.SetActive(true);
            playerUI.SetActive(true);

        } else if (mainMenu.activeInHierarchy == false) {

            mainMenu.SetActive(true);
            player.SetActive(false);
            playerUI.SetActive(false);

        }

    } 

    public void OnSoundButtonClick() {
        if (SoundManager.Instance != null) {
            Debug.Log("Toggle Sound from MainMenu");
            SoundManager.Instance.ToggleSound();
        }
    }

    public void OnExitButtonClick() {

        Debug.Log("Exit button clicked. Quitting application...");
        Application.Quit();

    } 

}
