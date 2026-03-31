using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Created By: Drew Oro
 * Created Date: 03/31/2026
 * Purpose: Manage the Sound, specifically the BackgroundMusic
 * 
 * Last Modified By: Drew Oro
 * Last Modified Date: 03/31/2026
 * Last Modified Made: Created SoundManager to get from MainMenu, PauseMenu, and Game Over Screen to apply changes
 */

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource backgroundMusic;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Sprite soundOnPressedSprite;
    public Sprite soundOffPressedSprite;

    private bool isSoundOn;
    private List<Button> registeredButtons = new List<Button>();

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        isSoundOn = PlayerPrefs.GetInt("IsSoundOn", 1) == 1;

        if (backgroundMusic != null) {
            if (isSoundOn) {
                if (!backgroundMusic.isPlaying) { backgroundMusic.Play(); }
            } else {
                backgroundMusic.Pause();
            }
        }
    }

    public bool IsSoundOn() {
        return isSoundOn;
    }

    public void RegisterButton(Button button)
    {
        if (button == null) return;

        if (!registeredButtons.Contains(button)) {
            registeredButtons.Add(button);
        }

        UpdateButtonSprite(button);
    }



    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("IsSoundOn", isSoundOn ? 1 : 0);

        if (backgroundMusic != null) {
            if (isSoundOn) {
                backgroundMusic.UnPause();
                if (!backgroundMusic.isPlaying) { backgroundMusic.Play(); }
            } else {
                backgroundMusic.Pause();
            }
        }

        StartCoroutine(UpdateSpritesNextFrame());

        Debug.Log("Sound toggled to: " + (isSoundOn ? "ON" : "OFF"));
    }

    public void UpdateButtonSprite(Button button)
    {
        if (button == null || button.image == null) return;

        Sprite sprite = isSoundOn ? soundOnSprite : soundOffSprite;
        Sprite pressedSprite = isSoundOn ? soundOnPressedSprite : soundOffPressedSprite;

        if (sprite != null) {
            button.image.sprite = sprite;
        }

        if (pressedSprite != null) {
            SpriteState spriteState = button.spriteState;
            spriteState.pressedSprite = pressedSprite;
            button.spriteState = spriteState;
        }
    }

    private void UpdateAllButtonSprites()
    {
        foreach (Button btn in registeredButtons) {
            UpdateButtonSprite(btn);
        }
    }

    private IEnumerator UpdateSpritesNextFrame()
    {
        yield return null;
        UpdateAllButtonSprites();
    }
}
