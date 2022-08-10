using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gManager;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    public int currentLevel = 0;
    private void Start()
    {
        Application.targetFrameRate = 30;
        UpdateGameState(GameState.Menu);
    }

    private void Awake()
    {
        gManager = this;
    }
    public void UpdateGameState(GameState newState)
    {
        this.state = newState;

        switch (newState)
        {
            case GameState.Menu:
                break;
            case GameState.Highscores:
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(newState);

    }

    private void HandleLose()
    {
        currentLevel = 0;
    }

    private void HandleWin()
    {
        currentLevel++;
        if(currentLevel >= LevelManager.lManager.levelList.level.Length)
        {
            currentLevel = 0;
        }
    }
}

    
public enum GameState
{
    Menu,
    Highscores,
    Playing,
    Paused,
    Win,
    Lose
}
