using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    public AudioSource ASource;

    public AudioClip rubberStretch;
    public AudioClip shotFire;

    public AudioClip bombHit;

    //when a cake was hit!
    public AudioClip cakeHit;









    void playSound(AudioClip clip)
    {
        ASource.PlayOneShot(clip);

    }

    public void OnBombHit()
    {
        playSound(bombHit);

    }

    public void OnShotFire()
    {
        ASource.Stop();
        playSound(shotFire);

    }

    public void OnSlingStretch()
    {
        playSound(rubberStretch);

    }

    public void OnCakeHit()
    {
        playSound(cakeHit);
    }


}

