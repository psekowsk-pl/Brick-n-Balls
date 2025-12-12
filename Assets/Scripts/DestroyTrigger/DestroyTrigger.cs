using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    private BallSpawner BallSpawner;

    private void Start()
    {
        BallSpawner = FindFirstObjectByType<BallSpawner>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BallManager>())
        {
            Destroy(collision.gameObject);
            if (BallSpawner.OnBallDestroyed != null) BallSpawner.OnBallDestroyed(this, new System.EventArgs());
        }
    }
}
