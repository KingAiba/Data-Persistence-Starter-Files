using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    public TMP_InputField nameField;
    public TextMeshProUGUI hightScoreText;
    public GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        UpdateHighScoreText();
    }

    
    void Update()
    {
        
    }

    public void OnNameFieldChange()
    {
        gameManager.playerName = nameField.text;
    }

    public void OnStartButtonClick()
    {
        gameManager.StartGame();
    }

    public void OnQuitButtonClick()
    {
        gameManager.QuitGame();
    }

    public void UpdateHighScoreText()
    {
        hightScoreText.SetText("HightScore : " + gameManager.highScore.name + " : " + gameManager.highScore.score);
    }
}
