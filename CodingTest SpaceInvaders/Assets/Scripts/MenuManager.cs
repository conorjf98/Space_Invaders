using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuUI, playingUI, pauseUI, winUI, loseUI, highscoreUI;


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
        menuUI.SetActive(state == GameState.Menu);
        pauseUI.SetActive(state == GameState.Paused);
        winUI.SetActive(state == GameState.Win);
        loseUI.SetActive(state == GameState.Lose);
        highscoreUI.SetActive(state == GameState.Highscores);
    }

    public void CloseApplication()
    {
        SaveManager.sManager.SaveLocal();
        Application.Quit();
    }
}
