using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager> {

    public Text highScoreText;
    public Text scoreText;

    private float score = 0;

    public float Score
    {
        get { return score; }
    }

    public void Start()
    {
        scoreText.text = score.ToString();
        highScoreText.text = String.Format("HighScore: {0}", PlayerPrefs.GetFloat("highScore")); 
    }

    public void AddScore(int score)
    {
        try
        {
            checked
            {
                this.score += score;
                scoreText.text = String.Format("Score: {0}" , this.score);
            }
        }
        catch(OverflowException)
        {
            Debug.Log("overflow!");
            this.score = float.MaxValue;
            GameManager.Instance.GameOver();
        }

        
    }


    public bool DisplayScore()
    {
        float hs = PlayerPrefs.GetFloat("highScore");
        float score = ScoreManager.Instance.Score;
        if (score > hs)
        {
            PlayerPrefs.SetFloat("highScore", ScoreManager.Instance.Score);

            if (score == float.MaxValue)
            {
                MenuManager.Instance.GameOverMenu(true, score, true);
                return true;
            }

            MenuManager.Instance.GameOverMenu(true, score);
            return true;
        }

        MenuManager.Instance.GameOverMenu(false, score);
        return false;
    }


}
