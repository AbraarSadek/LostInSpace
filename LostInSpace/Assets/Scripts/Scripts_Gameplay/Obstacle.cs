using UnityEngine;

public class Obstacle : MonoBehaviour
{

    Rigidbody2D rb; 

    void Start()
    {
        float randomSize = Random.Range(0.5f, 2.0f);
        transform.localScale = new Vector3(randomSize, randomSize, 1);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * 100);
    }

    void Update()
    {
        
    }
}
