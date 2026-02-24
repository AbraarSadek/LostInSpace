using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {


    //Private Variables
    private Rigidbody2D rb; //Rigidbody2D variable that will be used to apply physics forces to the player GameObject
    private float elapsedTime = 0f; //Float variable that will keep track of how much time has passed since the game started
    private float score = 0f; //Float variable that will keep track of the player's score, which increases over time based on how long they survive
    public float scoreMultiplier = 10f; //Float variable that determines how much the score increases per second (score increase rate)
    private Label scoreText; //Label variable that will be used to display the player's score on the UI
    //Public Variables
    public float thrustForce = 1f; //Float variable that determines how much force is applied to the player when thrusting (moving towards the mouse)
    public UIDocument uIDocument; //Reference to the UI Document component that holds the UI elements for displaying the score
    public GameObject explosionEffect;
    private Button restartButton;


    void Start()
    {
        // Grab the Rigidbody2D component attached to this Player GameObject
        rb = GetComponent<Rigidbody2D>();
        scoreText = uIDocument.rootVisualElement.Q<Label>("ScoreLabel"); //Get the Label element from the UI Document to display the score
        restartButton = uIDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;


    }

    void Update() {

        UpdateScore(); //Call the UpdateScore method to update the player's score based on elapsed time and score multiplier

        MovePlayer(); //Call the MovePlayer method to handle player movement towards the mouse position when the left mouse button is pressed
    
    }

    //UpdateScore Method - This method is responsible for updating the player's score based on the elapsed time and the score multiplier.
    void UpdateScore() {

        elapsedTime += Time.deltaTime; //Increment elapsed time by the time that has passed since the last frame
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier); //Calculate the player's score based on elapsed time and the score multiplier
        Debug.Log("Score: " + score);
        scoreText.text = "Score: " + score; //Update the score text in the UI to reflect the current score

    } //End of UpdateScore Method

    //MovePlayer Method - This method is responsible for moving the player towards the mouse position when the left mouse button is pressed.
    void MovePlayer() {

        //If-Statement - That Will Check If The Left Mouse Button Is Being Pressed (Held Down)
        if (Mouse.current.leftButton.isPressed) {

            // Get mouse position in SCREEN coordinates (pixels)
            Vector3 mouseScreenPos = Mouse.current.position.value;

            // Convert mouse position from SCREEN space to WORLD space
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            // Direction from player -> mouse (as a Vector2 for 2D)
            Vector2 direction = (mouseWorldPos - transform.position).normalized;

            // Rotate the player so its "up" direction points toward the mouse
            transform.up = direction;

            // Apply force in that direction (thrust)
            rb.AddForce(direction * thrustForce);

        } //End of If-Statement

    } //End of MovePlayer Method

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the player if it collides with anything
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        restartButton.style.display = DisplayStyle.Flex;


    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
