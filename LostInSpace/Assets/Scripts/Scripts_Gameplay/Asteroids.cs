using UnityEngine;

//Asteroids Class - Responsible for controlling the behavior of the asteroid GameObjects in the game, including their size, movement, and rotation when they are spawned.
public class Asteroids : MonoBehaviour {

    Rigidbody2D rb; //Rigidbody2D variable that will be used to apply physics forces to the asteroid GameObject

    //Public Variables - These variables can be adjusted in the Unity Inspector to control the behavior of the asteroids when they are spawned
    public float minSize = 1f;
    public float maxSize = 2.25f;
    public float minForce = 100f;
    public float maxForce = 200f;
    public float torque = 10f;

    //Start Method - Called when an asteroid GameObject is instantiated.
    void Start() {

        //Randomize Size, Force, Torque, and Direction - When the asteroid is spawned,
            //it will be given a random size (between minSize and maxSize),
            //a random force (between minForce and maxForce) that is inversely proportional to its size (so larger asteroids move slower),
            //a random torque (between -torque and torque) to make it rotate,
            //and a random direction (using Random.insideUnitCircle to get a random point inside a unit circle for 2D movement).
        float randomSize = Random.Range(minSize, maxSize);
        float randomForce = Random.Range(minForce, maxForce) / randomSize;
        float randomTorque = Random.Range(-torque, torque);

        //Random Direction - Get a random direction for the asteroid to move in by generating a random point inside a unit circle (2D) using Random.insideUnitCircle,
            //which returns a Vector2 with x and y components that represent the direction of movement for the asteroid.
        Vector2 randomDirection = Random.insideUnitCircle;

        //Apply Random Size, Force, Torque, and Direction - Set the asteroid's scale to the random size, apply the random force in the random direction using Rigidbody2D.AddForce,
            //and apply the random torque using Rigidbody2D.AddTorque to make the asteroid rotate.
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        //Grab the Rigidbody2D component attached to this asteroid GameObject,
            //and apply the random force and torque to it to make it move and rotate in the game world.
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(randomDirection * randomForce);
        rb.AddTorque(randomTorque);

    } //End of Start Method

} //End of Asteroids Class