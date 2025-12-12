using UnityEngine;

public class BallManager : MonoBehaviour
{
    public float Speed = 400f;
    
    private Rigidbody BallRB;
    private Vector2 BallVector;    
    
    private void Start()
    {
        BallRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        BallRB.linearVelocity = BallVector * Speed * Time.deltaTime;
    }

    // OnCollision bounce
    private void OnCollisionEnter(Collision collision)
    {
        BallVector = Vector2.Reflect(BallVector, collision.contacts[0].normal);
    }

    public void SetVector(Vector2 newVector)
    {
        BallVector = newVector;
    }
}
