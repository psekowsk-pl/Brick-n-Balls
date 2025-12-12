using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public ScoreSystem ScoreSystem;
    public GameObject GamePrefab;

    private GameObject MainGameObject;
    private UISceneManager UISceneManager;
    private BallSpawner BallSpawner;
    private BrickManager[] BrickManager;

    private GameFinishResult GameResult;
    private int NumberOfBalls;
    private int NumberOfBricks;
    
    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        if (newScene == SceneManager.GetSceneAt((int)ScenesEnum.MainGame))
        {
            UISceneManager = FindFirstObjectByType<UISceneManager>();
            ScoreSystem = new();
            MainGameObject = Instantiate(GamePrefab, transform.position, transform.rotation);

            BallSpawner = FindFirstObjectByType<BallSpawner>();
            NumberOfBalls = BallSpawner.GetMaxNumberOfBalls();
            BallSpawner.OnBallDestroyed += CheckBallsStatus_OnBallDestroyed;

            NumberOfBricks = FindObjectsByType<BrickManager>(FindObjectsSortMode.None).Count();

            BrickManager = FindObjectsByType<BrickManager>(FindObjectsSortMode.None);
            foreach (var brickManager in BrickManager)
            {
                brickManager.OnBrickDestroyed += CheckBricksStatus_OnBrickDestroyed;
            }
        }
        else
        {
            Destroy(MainGameObject);

            ScoreSystem = null;
            BallSpawner = null;
            UISceneManager = null;
        } 
    }

    private void CheckBallsStatus_OnBallDestroyed(object sender, EventArgs e)
    {
        NumberOfBalls--;
        if (NumberOfBalls <= 0 && NumberOfBricks > 0)
        {
            GameResult = GameFinishResult.Lose;
            Destroy(MainGameObject);
            UISceneManager.FinishGame();
        }
        else if (NumberOfBalls <= 0 && NumberOfBalls <= 0)
        {
            Destroy(MainGameObject);
            UISceneManager.FinishGame();
        }
    }

    private void CheckBricksStatus_OnBrickDestroyed(object sender, EventArgs e)
    {
        NumberOfBricks--;
        if (NumberOfBricks <= 0)
        {
            GameResult = GameFinishResult.Won;
        }
    }

    public string GetResult()
    {
        if (GameResult == GameFinishResult.Won)
        {
            return "Level Complete!";
        }
        else
        {
            return "Game Over!";
        }
    }
}
