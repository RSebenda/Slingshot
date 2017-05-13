using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager> {

    public Text scoreText;

    private int score = 0;

    public int Score
    {
        get { return score; }
    }

    public void Start()
    {
        scoreText.text = score.ToString();

    }

    public void AddScore(int score)
    {
        try
        {
            checked
            {
                this.score += score;
                scoreText.text = String.Format("Score: {0}" , this.score.ToString());
            }
        }
        catch(OverflowException ex)
        {

            GameManager.Instance.GameOver();
        }

        
    }



}
