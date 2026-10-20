using Unity.AI.Assistant.FunctionCalling;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    [SerializeField]
    private ScoreUI scoreUI;

    private int score;

    public int Score
    {
        get
        {
            return score;
        }
    }

    private void Start()
    {
        if (GameSession.Instance != null)
        {
            score = GameSession.Instance.Score;
        }
        UpdateUI();
        if(PersistentGameManager.Instance != null)
        {
            PersistentGameManager.Instance.UpdateHighScore(score);
        }
    }

    public void AddScore(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        score += amount;
        if (GameSession.Instance != null)
        {
            GameSession.Instance.Score = score;
        }
        if(PersistentGameManager.Instance != null)
        {
            PersistentGameManager.Instance.UpdateHighScore(score);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreUI != null)
        {
            scoreUI.SetScore(score);
        }
    }
}
