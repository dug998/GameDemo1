using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Game : MonoBehaviour
{ 
    public TextMeshProUGUI score;
    public void playGame()
    {
        score.text = PlayerPrefs.GetFloat("Score",0).ToString();
        SceneManager.LoadScene(0);
    }
   
    bool pause = false;
    public void Pause()
    {
        pause = !pause;
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

}
