using UnityEngine;

public class Obstacle : MonoBehaviour
{

    Rigidbody2D rb;
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float minForce = 75f;
    public float maxForce = 200f;
    public float torque = 10f;

    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize);
        float randomForce = Random.Range(minForce, maxForce) / randomSize;
        float randomTorque = Random.Range(-torque, torque);
        Vector2 randomDirection = Random.insideUnitCircle;
        transform.localScale = new Vector3(randomSize, randomSize, 1);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(randomDirection * randomForce);
        rb.AddTorque(randomTorque);
    }

}
