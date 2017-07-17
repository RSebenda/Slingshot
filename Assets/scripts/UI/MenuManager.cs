using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class MenuManager : Singleton<MenuManager> {


    public Stack<UIScreen> screens;

    //set this var to show the first screen per scene
    public UIScreen firstScreen;

    void Awake()
    {
        Time.timeScale = 1;
        screens = new Stack<UIScreen>();
        //always get the MainScreen First
       
        screens.Push(firstScreen);
        screens.Peek().gameObject.SetActive(true);


    }


  

    public virtual void Show(UIScreen screen)
    {
       
        screens.Peek().gameObject.SetActive(false);
        screen.gameObject.SetActive(true);
        screens.Push(screen);


    }

    public virtual void Close()
    {
        
        
            UIScreen screen = screens.Pop();
            screen.gameObject.SetActive(false);
            screens.Peek().gameObject.SetActive(true);
        

    }


    public void GameOverMenu(bool isHighScore, float score,  bool isMaxScore = false)
    {

        var screen = GetComponentInChildren<GameOverScreen>(true);
        Debug.Log(screen);
        if(isMaxScore)
        {
            screen.SetData("WoW! You got the highest score possible, Thanks for playing!", score );
            
        }
        else if (isHighScore)
        {
            screen.SetData("New High Score!", score );
            

        }
        else //not a high score
        {
            screen.SetData("Your Score was", score );
            


        }

        this.Show(screen);


    }

    //go back to MainScreen menu, play ad
    public void OnGameEnd()
    {

        SceneManager.LoadScene("MainMenu");

#if UNITY_ADS
        if(Advertisement.IsReady())
        {
            Advertisement.Show();
        }
#endif    


    }




}
