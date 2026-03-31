using UnityEngine;
using UnityEngine.UIElements;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    private void Start()
    {
        
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
}
