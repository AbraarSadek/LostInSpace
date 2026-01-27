using UnityEngine;

public class Obstacle : MonoBehaviour
{

    Rigidbody2D rb; 

    void Start()
    {
        float randomSize = Random.Range(0.5f, 2.0f);
        float randomForce = Random.Range(25f, 200f);
        transform.localScale = new Vector3(randomSize, randomSize, 1);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * randomForce);
    }

    void Update()
    {
        
    }
}
