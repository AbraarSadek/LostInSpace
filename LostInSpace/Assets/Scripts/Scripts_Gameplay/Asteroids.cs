using UnityEngine;

/*
 * Created By: Abraar Sadek
 * Created Date: N/A
 * Purpose: Responsible for controlling the behavior of the asteroid GameObjects in the game, including their size, movement, and rotation when they are spawned.
 * 
 * Last Modified By: Drew Oro
 * Last Modified Date: 04/07/2026
 * Last Modified Made: Converted the public float variables into private float variables and removed comments.
 */

public class Asteroids : MonoBehaviour {

    Rigidbody2D rb; 
    private float minSize = 1f;
    private float maxSize = 2.25f;
    private float minForce = 300f;
    private float maxForce = 500f;
    private float torque = 40f;

    void Start() {

        //Randomize Size, Force, Torque, and Direction - When the asteroid is spawned,
        float randomSize = Random.Range(minSize, maxSize);
        float randomForce = Random.Range(minForce, maxForce) / randomSize;
        float randomTorque = Random.Range(-torque, torque);
        Vector2 randomDirection = Random.insideUnitCircle;

        //Apply Random Size, Force, Torque, and Direction
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(randomDirection * randomForce);
        rb.AddTorque(randomTorque);

    }

}