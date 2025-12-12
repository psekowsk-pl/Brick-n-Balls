using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISceneManager : MonoBehaviour
{
    public Canvas ScoreCanvas;
    public TextMeshProUGUI ScoreText;
    
    public Canvas StartGameCanvas;
    public Button StartGameButton;

    public Canvas GameFinishedCanvas;
    public TextMeshProUGUI GameFinishedResultText;
    public TextMeshProUGUI GameFinishedScoreText;
    public Button ReturnToMainMenuButton;

    private GameHandler GameHandler;
    private Scene UIScene;
    private Scene MainGameScene;

    private void Start()
    {
        StartGameCanvas.enabled = true;
        StartGameButton.onClick.AddListener(StartGame);
        ReturnToMainMenuButton.onClick.AddListener(GoBackToMainMenu);
        UIScene = SceneManager.GetSceneAt((int)ScenesEnum.UIScene);
        MainGameScene = SceneManager.GetSceneAt((int)ScenesEnum.MainGame);
        SceneManager.SetActiveScene(UIScene);
    }

    // UI Canvas logic
    private void StartGame()
    {
        StartGameCanvas.enabled = false;
        ScoreCanvas.enabled = true;

        // Activate MainGameScene
        SceneManager.SetActiveScene(MainGameScene);
        GameHandler = FindFirstObjectByType<GameHandler>();
        GameHandler.ScoreSystem.OnScoreChanged += UISceneManager_OnScoreChanged;
        SetUIScore();
    }

    public void FinishGame()
    {
        ScoreCanvas.enabled = false;
        GameFinishedCanvas.enabled = true;
        GameFinishedResultText.text = GameHandler.GetResult();
        GameFinishedScoreText.text = $"Final Score: {GameHandler.ScoreSystem.GetScore()}";

        // Activate UIScene
        SceneManager.SetActiveScene(UIScene);
    }

    private void GoBackToMainMenu()
    {
        GameFinishedCanvas.enabled = false;
        ScoreCanvas.enabled = false;
        StartGameCanvas.enabled = true;
    }

    // UI Score text manager
    public void UISceneManager_OnScoreChanged(object sender, EventArgs e)
    {
        SetUIScore();
    }

    private void SetUIScore()
    {
        ScoreText.text = GameHandler.ScoreSystem.GetScore().ToString();
    }
}
