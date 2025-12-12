using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallSpawner : MonoBehaviour
{
    public EventHandler OnBallDestroyed;
    public GameObject BallPrefab;
    private GameObject CurrentBall;
    private PlayerInputActions PlayerInput;

    private Camera Cam;
    private Vector2 MouseLocation;
    private Vector2 BallVector;

    private bool IsFired = false;
    private int MaxNumberOfBalls = 10;
    private int BallIdToGenerate = 0;
    private float BallsSpawnDelay = .2f;
    private float CurrentBallsSpawnDelay = 0f;

    // Draw line, where you are aiming
    public LineRenderer AimLine;
    public float AimLength = 3f;

    private void Start()
    {
        Cam = Camera.main;
        CurrentBall = Instantiate(BallPrefab, transform.position, transform.rotation);
        SetBallColor(CurrentBall);

        // Set player input
        PlayerInput = new();
        PlayerInput.Enable();
        PlayerInput.Gameplay.Click.performed += SetVectorAndFire_OnFired;
    }

    private void Update()
    {
        // Shoot multiple balls logic
        if (IsMaxNumberOfBallsReached())
        {
            return;
        }

        if (CurrentBallsSpawnDelay > 0f)
        {
            CurrentBallsSpawnDelay -= Time.deltaTime;
        }
        else if (IsFired)
        {
            ShootBall();
        }

        // Draw line
        if (!IsFired && CurrentBall != null)
        {
            MouseLocation = Cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (MouseLocation - (Vector2)CurrentBall.transform.position).normalized;
            DrawShortLine(CurrentBall.transform.position, direction);
        }
        else
        {
            AimLine.positionCount = 0;
        }
    }

    private void SetVectorAndFire_OnFired(InputAction.CallbackContext ctx)
    {
        ShootBall();
        PlayerInput.Gameplay.Click.performed -= SetVectorAndFire_OnFired;
        PlayerInput.Disable();
    }

    private void ShootBall()
    {
        if (!IsFired)
        {
            IsFired = true;
        }
            
        // Get cursor locator, create ball and set vector
        if (BallVector.Equals(Vector2.zero))
        {
            BallVector = math.normalize(MouseLocation - new Vector2(transform.position.x, transform.position.y));
        }

        CurrentBall.GetComponent<BallManager>().SetVector(BallVector);
        BallIdToGenerate++;

        if (IsMaxNumberOfBallsReached())
        {
            return;
        }

        // Create new instance of the new ball
        CurrentBall = Instantiate(BallPrefab, transform.position, transform.rotation);
        SetBallColor(CurrentBall);

        // Set spawn delay
        CurrentBallsSpawnDelay = BallsSpawnDelay;
    }

    private bool IsMaxNumberOfBallsReached()
    {
        if (BallIdToGenerate >= MaxNumberOfBalls)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void SetBallColor(GameObject currentBall)
    {
        switch (BallIdToGenerate % 4)
        {
            case 0:
                currentBall.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 1:
                currentBall.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case 2:
                currentBall.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case 3:
                currentBall.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
        }
    }

    public int GetMaxNumberOfBalls() => MaxNumberOfBalls;

    private void DrawShortLine(Vector2 startPos, Vector2 direction)
    {
        Vector3 endPos = startPos + direction * AimLength;

        AimLine.positionCount = 2;
        AimLine.SetPosition(0, startPos);
        AimLine.SetPosition(1, endPos);
    }
}
