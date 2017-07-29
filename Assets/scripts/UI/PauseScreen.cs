using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : UIScreen {


    public Sprite audioButton;
    public Sprite mutedAudioButton;

    public Button UIButton;

    public void Awake()
    {
        UpdateAudioButton();
    }


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


    public void ToggleAudio()
    {
        AudioManager.muted = !AudioManager.muted;

        UpdateAudioButton();
   
    }

    //shows the correct Button in the puase screen
    public void UpdateAudioButton()
    {
        bool muted = AudioManager.muted;


        if(muted)
        {
            UIButton.image.sprite = mutedAudioButton;
        }
        else
        {
            UIButton.image.sprite = audioButton;
        }
    }

}
