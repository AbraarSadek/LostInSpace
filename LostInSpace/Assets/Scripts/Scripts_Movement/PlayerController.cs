using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEditor;

/*
 * Created By: Abraar Sadek
 * Created Date: N/A
 * Purpose: Responsible for controlling the player's movement towards the mouse position when the left mouse button is pressed, as well as keeping track of the player's score based on how long they survive in the game.
 * It also handles player collisions and allows for restarting the game after a collision.
 * 
 * Last Modified By: Drew Oro
 * Last Modified Date: 04/07/2026
 * Last Modified Made: Code Check for Quality Assurance
 */
public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private float elapsedTime = 0f;
    private float score = 0f;
    private Label scoreText;
    private Button restartButton;
    private float scoreCount = 0f;
    private float scoreSpawnRate = 10f;
    private float maxScoreSpawnRate = 200f;
    private GameOverManager gameOverManager;
    private MainMenuManager mainMenuManager;

    public float thrustForce = 1f;
    public UIDocument uIDocument;
    public GameObject explosionEffect;
    public GameObject boosterFlame;
    public AudioSource thrusterAudio;
    public AudioSource backgroundAudio;
    public GameObject borderParent;
    public GameObject asteroidPrefab;
    public PhysicsMaterial2D AsteroidMaterial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uIDocument.rootVisualElement.Q<Label>("ScoreLabel");
        gameOverManager = FindFirstObjectByType<GameOverManager>();
        mainMenuManager = FindFirstObjectByType<MainMenuManager>();
        SoundManager.Instance.ApplySound();
    }

    void Update()
    {
        UpdateScore();
        MovePlayer();
    }

    void SpawnAsteroid()
    {
        if (score - scoreCount >= scoreSpawnRate)
        {
            Instantiate(asteroidPrefab);
            scoreCount = score;
            if (scoreSpawnRate <= maxScoreSpawnRate)
            {
                scoreSpawnRate = scoreSpawnRate * 1.5f;
                AsteroidMaterial.bounciness += 0.05f;
            }
        }
    }
    void UpdateScore()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * 1.25f);
        scoreText.text = "Score: " + score;
        SpawnAsteroid();

    }
    void MovePlayer()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 mouseScreenPos = Mouse.current.position.value;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            Vector2 direction = (mouseWorldPos - transform.position).normalized;
            transform.up = direction;
            rb.AddForce(direction * thrustForce);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
            thrusterAudio.Play();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
            thrusterAudio.Stop();
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        Instantiate(explosionEffect, transform.position, transform.rotation);
        mainMenuManager.audioSoundEffects.Add(explosionEffect.GetComponent<AudioSource>());
        SoundManager.Instance.ApplySound();
        borderParent.SetActive(false);
        gameObject.SetActive(false);
        uIDocument.enabled = false;
        gameOverManager.ApplyScore(score);
        Debug.Log("Game Over (Player Controller)");
        gameOverManager.activateGameOverPanel(true);

    }

}