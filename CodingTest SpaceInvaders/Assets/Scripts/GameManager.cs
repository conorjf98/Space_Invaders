using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gManager;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
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
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(newState);

    }

    
}

    
public enum GameState
{
    Menu,
    Playing,
    Paused,
    Win,
    Lose
}
