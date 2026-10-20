using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI highScoreText;

    private void Start()
    {
        int highScore = 0;

        if(PersistentGameManager.Instance != null)
        {
            highScore = PersistentGameManager.Instance.HighScore;
        }

        if(highScoreText != null)
        {
            highScoreText.text = "HighScore: " + highScore;
        }
    }
}
