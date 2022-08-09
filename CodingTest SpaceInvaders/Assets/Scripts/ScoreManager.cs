using TMPro;
using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager sManager;
    [SerializeField]
    int currentScore;
    [SerializeField]
    int highscore;
    [SerializeField]
    TextMeshProUGUI currentScoreText;
    [SerializeField]
    TextMeshProUGUI HighscoreText;

    private void Start()
    {
        sManager = this;
    }
    public void AddScore(int amount)
    {
        currentScore += amount;
        currentScoreText.text = currentScore.ToString();

        if(currentScore > highscore)
        {
            highscore = currentScore;
            HighscoreText.text = highscore.ToString();
        }
    }
}
