using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public List<Highscore> savedHighscores;
    public static SaveManager sManager;

    private void Awake()
    {
        sManager = this;
    }
    public void Start()
    {
        LoadLocal();
        //DeleteLocal();
    }
    public void SaveLocal()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/highscoreData.dat");
        savedHighscores.Sort(SortByScoreDescending);
        highscoreSaveData h_data = StoreGameVariableToHighscoreData();
        //end save data
        bf.Serialize(file, h_data);
        file.Close();
    }

    public void LoadLocal()
    {
        Debug.Log("Attempting to load local");
        if (File.Exists(Application.persistentDataPath + "/highscoreData.dat"))
        {
            Debug.Log("FoundFile");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/highscoreData.dat", FileMode.Open);
            highscoreSaveData h_data = (highscoreSaveData)bf.Deserialize(file);
            file.Close();
            StoreHighscoreDataToGameVariables(h_data);
        }
        else
        {
            Debug.Log("Starting Initial Setup...");
            SaveLocal();
        }
    }

    [Serializable]
    public class highscoreSaveData
    {
        public List<Highscore> _savedHighscores = new List<Highscore>();
    }

    public void StoreHighscoreDataToGameVariables(highscoreSaveData p_data)
    {
        savedHighscores = p_data._savedHighscores;
        savedHighscores.Sort(SortByScoreDescending);
    }

    public highscoreSaveData StoreGameVariableToHighscoreData()
    {
        highscoreSaveData p_data = new highscoreSaveData();
        p_data._savedHighscores = savedHighscores;
        return p_data;
    }

    public void DeleteLocal()
    {
        if (File.Exists(Application.persistentDataPath + "/highscoreData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/highscoreData.dat");
        }
    } 

    public void CheckScore(Highscore hs)
    {
        if (savedHighscores.Count < 3)
        {
            savedHighscores.Add(hs);
            SaveLocal();
        }
        else
        {
            savedHighscores.Sort(SortByScoreAscending);
            foreach (Highscore savedHighScore in savedHighscores)
            {
                if(savedHighScore.score < hs.score)
                {
                    savedHighScore.score = hs.score;
                    savedHighScore.playerName = hs.playerName;
                    SaveLocal();
                    break;
                }
            }
        }
    }

    static int SortByScoreDescending(Highscore p1, Highscore p2)
    {
        return p2.score.CompareTo(p1.score);
    }

    static int SortByScoreAscending(Highscore p1, Highscore p2)
    {
        return p1.score.CompareTo(p2.score);
    }
}
