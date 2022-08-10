using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuUI, playingUI, pauseUI, winUI, loseUI;


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

    }

    public void CloseApplication()
    {
        Application.Quit();
    }
}
