using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject mainMenuPanel;
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




    public void OnHomeButtonClick()
    {

        Debug.Log("Home button clicked. Heading Home");
        //gameOverPanel.SetActive(false);
        //mainMenuPanel.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    } 
}
