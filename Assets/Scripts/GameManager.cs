using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public struct HighScoreEntry
{
    public string name;
    public int score;

    public HighScoreEntry(string Name, int Score)
    {
        name = Name;
        score = Score;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName;

    public HighScoreEntry highScore;

    private void InitializeGameManager()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateHighScore(int score)
    {
        if(highScore.score <= score)
        {
            highScore = new HighScoreEntry(playerName, score);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void Awake()
    {
        InitializeGameManager();
        LoadScoreData();
    }

    [System.Serializable]
    class SaveData
    {
        public HighScoreEntry highestScore;
    }

    public void SaveScoreData()
    {
        SaveData data = new SaveData();
        data.highestScore = highScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScoreData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highestScore;
        }
    }
}
