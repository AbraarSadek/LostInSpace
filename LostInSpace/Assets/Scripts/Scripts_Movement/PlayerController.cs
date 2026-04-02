using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

//PlayerController Class - Responsible for controlling the player's movement towards the mouse position when the left mouse button is pressed, as well as keeping track of the player's score based on how long they survive in the game.
  //It also handles player collisions and allows for restarting the game after a collision.
public class PlayerController : MonoBehaviour {

  //Private Variables
  private Rigidbody2D rb; //Rigidbody2D variable that will be used to apply physics forces to the player GameObject
  private float elapsedTime = 0f; //Float variable that will keep track of how much time has passed since the game started
  private float score = 0f; //Float variable that will keep track of the player's score, which increases over time based on how long they survive
  private Label scoreText; //Label variable that will be used to display the player's score on the UI
  private Button restartButton; //Reference to the Button element in the UI Document that will be used to restart the game after a player collision

  //Public Variables
  public float thrustForce = 1f; //Float variable that determines how much force is applied to the player when thrusting (moving towards the mouse)
  public UIDocument uIDocument; //Reference to the UI Document component that holds the UI elements for displaying the score
  public float scoreMultiplier = 10f; //Float variable that determines how much the score increases per second (score increase rate)
  public GameObject explosionEffect; //Reference to the explosion effect prefab that will be instantiated when the player collides with an asteroid or other obstacle
  public GameObject boosterFlame; //Reference to the booster flame GameObject that will be activated when the player moving.
    public AudioSource thrusterAudio;
    public AudioSource backgroundAudio;
  public GameObject borderParent; //Reference to the parent GameObject that contains the border colliders to prevent the player from moving off-screen
    private GameOverManager gameOverManager;
    private MainMenuManager mainMenuManager;
  //Start Method - Called when the player GameObject is instantiated.
  void Start() {

    //Grab the Rigidbody2D component attached to this Player GameObject
    rb = GetComponent<Rigidbody2D>();

    //Get the Label element from the UI Document to display the score
    scoreText = uIDocument.rootVisualElement.Q<Label>("ScoreLabel");
    //Get the Button element from the UI Document to handle restarting the game after a player destruction
    restartButton = uIDocument.rootVisualElement.Q<Button>("RestartButton"); 
    restartButton.style.display = DisplayStyle.None;
    restartButton.clicked += ReloadScene;
        
        gameOverManager = FindFirstObjectByType<GameOverManager>();
        mainMenuManager = FindFirstObjectByType<MainMenuManager>();
        SoundManager.Instance.ApplySound();
  } //End of Start Method

  //Update Method - Called once per frame to handle player movement and score updates.
  void Update() {

    UpdateScore(); //Call the UpdateScore method

    MovePlayer(); //Call the MovePlayer method

  } //End of Update Method

  //UpdateScore Method - Responsible for updating the player's score based on the elapsed time and the score multiplier.
  void UpdateScore() {

    elapsedTime += Time.deltaTime; //Increment elapsed time by the time that has passed since the last frame
    score = Mathf.FloorToInt(elapsedTime * scoreMultiplier); //Calculate the player's score based on elapsed time and the score multiplier
    
    //Debug.Log("Score: " + score);

    scoreText.text = "Score: " + score; //Update the score text on the UI to reflect the current score

  } //End of UpdateScore Method

  //MovePlayer Method - Responsible for moving the player towards the mouse position when the left mouse button is pressed.
  void MovePlayer() {

    //If-Statement - That Will Check If The Left Mouse Button Is Being Pressed (Held Down)
    if (Mouse.current.leftButton.isPressed) {

      Vector3 mouseScreenPos = Mouse.current.position.value; //Get mouse positions on screen coordinates 

      Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos); //Convert mouse position from screen space to world space

      //Calculate the direction from the player to the mouse position by subtracting the player's position from the mouse world position,
        //and then normalizing it to get a unit vector that represents the direction of movement for the player.
      Vector2 direction = (mouseWorldPos - transform.position).normalized;

      //Rotate the player to face the direction of movement by setting the player's up vector to the calculated direction vector.
      transform.up = direction;

      //Apply a force to the player's Rigidbody2D in the direction of movement,
        //multiplied by the thrust force variable to control how fast the player moves towards the mouse position.
      rb.AddForce(direction * thrustForce);

    } //End of If-Statement

    //If-Else Statement - That Will Check If The Left Mouse Button Was Pressed This Frame To Activate The Booster Flame, 
    //And Check If It Was Released This Frame To Deactivate The Booster Flame.
    if (Mouse.current.leftButton.wasPressedThisFrame) {
      boosterFlame.SetActive(true);
            thrusterAudio.Play();
    } else if (Mouse.current.leftButton.wasReleasedThisFrame) {
      boosterFlame.SetActive(false);
            thrusterAudio.Stop();
        } //End of If-Else Statement

  } //End of MovePlayer Method

  //OnCollisionEnter2D Method - Called when the player GameObject collides with another collider in the game world.
  void OnCollisionEnter2D(Collision2D collision) {

    //Instantiate explosion effect at player's position and rotation when a collision occurs,
      //then disable/hide the player GameObject instead of destroying it so it can be reused or inspected in editor,
      //and display the restart button on the UI to allow the player to restart the game after a collision.
    Instantiate(explosionEffect, transform.position, transform.rotation);
    mainMenuManager.audioSoundEffects.Add(explosionEffect.GetComponent<AudioSource>());
    SoundManager.Instance.ApplySound();
        borderParent.SetActive(false); //Disable the border colliders to allow the explosion effect to go off-screen without being blocked by the borders

    //Disable the player GameObject to hide it without destroying (preserves object for reuse or inspection)
    gameObject.SetActive(false); //Hide/disable the player GameObject upon collision
                                 //Show the restart button on the UI to allow the player to restart the game after a collision
                                 // restartButton.style.display = DisplayStyle.Flex;
        uIDocument.enabled = false;
        gameOverManager.ApplyScore(score);
        Debug.Log("Game Over (Player Controller)");
        gameOverManager.activateGameOverPanel(true); //Please change this with a variable at void Start!

  } //End of OnCollisionEnter2D Method

  //ReloadScene Method - Responsible for reloading the current scene to restart the game when the restart button is clicked.
  void ReloadScene() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Reload the current scene to restart the game
  }

} //End of PlayerController Class
