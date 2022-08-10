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
    TextMeshProUGUI HighscoreScoreText;
    [SerializeField]
    TextMeshProUGUI currentScoreNameText;
    [SerializeField]
    TextMeshProUGUI HighscoreNameText;
    [SerializeField]
    TMP_InputField nameInput;
    private void Start()
    {
        sManager = this;
    }

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        //reset the current score after a win or a loss
        if (state == GameState.Win || state == GameState.Lose) 
        {
            currentScore = 0;
            currentScoreText.text = currentScore.ToString();
        }


    }
    public void AddScore(int amount)
    {
        currentScore += amount;
        currentScoreText.text = currentScore.ToString();

        //if score is greater than highscore, update the highscore in realtime and change the name
        if(currentScore > highscore)
        {
            highscore = currentScore;
            HighscoreScoreText.text = highscore.ToString();
            HighscoreNameText.text = currentScoreNameText.text;
        }
    }

    public void SetName()
    {
        currentScoreNameText.text = nameInput.text;
    }
}
