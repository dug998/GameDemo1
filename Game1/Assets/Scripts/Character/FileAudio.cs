using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileAudio : MonoBehaviour
{
    public AudioSource audiosrc;
    public AudioClip  jumpSound;
  

    void Start() {
        //playerHitSound = Resources.Load<AudioClip>("playerHit");
        //firesound = Resources.Load<AudioClip>("fire");
        jumpSound = Resources.Load<AudioClip>("jump");
       //  enemyDeathSound = Resources.Load<AudioClip>("enemyDeath");
        //  audiośrc = GetComponent<AudioSource>();
    }
    public void Playsound(string clip)
    {
        switch (clip)
        {
            case "jump":
                audiosrc.PlayOneShot(jumpSound);
                break;
           
        }
    }
}
