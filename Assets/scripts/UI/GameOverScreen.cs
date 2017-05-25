using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : UIScreen {


    public Text mainText;
    public Text scoreText; 

    public void SetData(string topText, float score)
    {
        mainText.text = topText;
        scoreText.text = score.ToString();

    }


    public void OnReplay()
    {
        SceneManager.LoadScene("MainScene");

    }

    public void OnQuit()
    {
        MenuManager.Instance.OnGameEnd();

    }



}
