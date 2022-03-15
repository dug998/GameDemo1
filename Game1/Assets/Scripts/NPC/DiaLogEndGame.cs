using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DiaLogEndGame : MonoBehaviour
{
   
    public bool isDiglog = false;
 

    public TextMeshProUGUI textDisplay;
 

    // câu nhắc nhở khi chưa hoàn thành 
    public string[] sentencesRemind;
    
    // câu nói tạm biết game ;
    public string[] sentencesBye;

    private int index=0;
    public float typingSpeed;
    public GameObject ContinueButton;
    public GameObject GotoButton;
    bool Complete;
    void Start()
    {
        Continue(false);
        GotoButton.SetActive(false);

        for(int i = 1; i < 8; i++)
        {
           if(PlayerPrefs.GetInt("NgocRong" + i, 0) == 0)
            {
                Complete = false;
                return;
            }
            Complete = true;
        }
       
    }


    IEnumerator Type()
    {
        if (!Complete)
        {
            foreach (char letter in sentencesRemind[index].ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);

            }
        }
        else
        {
            foreach (char letter in sentencesBye[index].ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);

            }
        }
    }
    private void FixedUpdate()
    {
        if (isDiglog)
        {
            if (!Complete)
            {
                if (textDisplay.text == sentencesRemind[index].ToString())
                    Continue(true);
            }
            else
            {
                if (textDisplay.text == sentencesBye[index])
                    Continue(true);
            }

            
        }
    }
    public void Continue(bool x)
    {
        ContinueButton.SetActive(x);
    }
    public void NextSentence()
    {
        if (isDiglog)
        {
            if (!Complete)
            {
                if (index < sentencesRemind.Length -1)
                {
                    
                    textDisplay.text = "";
                    StartCoroutine(Type());
                    index++;

                }
                else
                {
                  
                    textDisplay.text = "";
                    Continue(false);
                    index = 0;
                }
            }
            else
            {
                if (index < sentencesBye.Length -1)
                {
                  

                    textDisplay.text = "";
                    StartCoroutine(Type());
                    index++;
                }
                else
                {
                    GotoButton.SetActive(true);
                    textDisplay.text = "";
                    Continue(false);
                    index = 0;
                }
            }

           
        }
    }
  
    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
