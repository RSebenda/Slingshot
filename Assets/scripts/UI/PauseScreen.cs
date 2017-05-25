using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : UIScreen {


    //on show
    private void OnEnable()
    {
        Time.timeScale = 0;

    }


    //on hide
    private void OnDisable()
    {
        Time.timeScale = 1;


    }

    public void OnQuit()
    {
        MenuManager.Instance.OnGameEnd();

    }

}
