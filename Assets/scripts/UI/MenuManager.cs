using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MenuManager : Singleton<MenuManager> {


    public Stack<UIScreen> screens;


    void Awake()
    {
        screens = new Stack<UIScreen>();
        //always get the MainScreen First
        screens.Push(FindObjectOfType<MainScreen>());
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
        //never pop first screen
        if(screens.Count >1)
        {
            UIScreen screen = screens.Pop();
            screen.gameObject.SetActive(false);
            screens.Peek().gameObject.SetActive(true);
        }

    }



}
