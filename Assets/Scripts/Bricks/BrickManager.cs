using System;
using TMPro;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public int BrickHealth = 3;
    public int AddScoreAmount = 10;

    public EventHandler OnBrickDestroyed;
    private EventHandler OnBrickHealthChange;

    private TextMeshProUGUI HealthText;
    private GameHandler GameHandler;
    private SpriteRenderer BrickSpriteRenderer;

    private void Start()
    {
        GameHandler = FindFirstObjectByType<GameHandler>();

        BrickSpriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        OnBrickHealthChange = UpdateBrickText_OnBrickHealthChange;
        OnBrickHealthChange += ChangeBrickMaterial_OnBrickHealthChange;

        HealthText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        HealthText.text = BrickHealth.ToString();
        if (OnBrickHealthChange != null) OnBrickHealthChange(this, new EventArgs());
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reduce health
        BrickHealth -= 1;
        if (OnBrickHealthChange != null) OnBrickHealthChange(this, new EventArgs());

        // Add points
        GameHandler.ScoreSystem.AddPointsToScore(AddScoreAmount);

        if (BrickHealth == 0)
        {
            if (OnBrickDestroyed != null) OnBrickDestroyed(this, new EventArgs());
            Destroy(gameObject);
        }
    }

    private void UpdateBrickText_OnBrickHealthChange(object sender, EventArgs e)
    {
        HealthText.text = BrickHealth.ToString();
    }

    private void ChangeBrickMaterial_OnBrickHealthChange(object sender, EventArgs e)
    {
        switch (BrickHealth)
        {
            case 3:
                BrickSpriteRenderer.color = Color.red;
                break;
            case 2:
                BrickSpriteRenderer.color = Color.yellow;
                break;
            case 1:
                BrickSpriteRenderer.color = Color.green;
                break;
            default:
                break;
        }
    }
}
