using System.Collections.Generic;
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
    [SerializeField]
    GameObject leaderboardPanel;
    [SerializeField]
    List<LeaderboardEntry> leaderboardEntries;
    [SerializeField]
    GameObject highscorePrefab;
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
            Highscore hs = new Highscore();
            hs.playerName = currentScoreNameText.text;
            hs.score = currentScore;
            SaveManager.sManager.CheckScore(hs);
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

    public void SetupScores()
    {
        currentScoreNameText.text = nameInput.text;
        HighscoreNameText.text = SaveManager.sManager.savedHighscores[0].playerName;
        highscore = SaveManager.sManager.savedHighscores[0].score;
        HighscoreScoreText.text = highscore.ToString();
    }

    public void SetupLeaderboards()
    {
        GameManager.gManager.UpdateGameState(GameState.Highscores);
        foreach ( Highscore highscore in SaveManager.sManager.savedHighscores)
        {
            GameObject highscoreObject = Instantiate(this.highscorePrefab, leaderboardPanel.transform);
            highscoreObject.GetComponent<LeaderboardEntry>().playerName.text = highscore.playerName;
            highscoreObject.GetComponent<LeaderboardEntry>().score.text = highscore.score.ToString();
        }
    }

    public void exitLeaderboards()
    {
        foreach (Transform child in leaderboardPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameManager.gManager.UpdateGameState(GameState.Menu);
    }
}
