using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


// hộp thoại
/*
 
 
khi nhân vật va chạm với npc sẽ lấy idNPC để lấy ra hộp hội thoại hợp lý
mở nút continue
 
 */
public class DiaLog : MonoBehaviour
{
    public bool isDiglog = false;
    public float IdNPC=0;
 
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI textGoto;


    public string[] sentences;
    private int index=0;
    public float typingSpeed;
    public GameObject ContinueButton;
    public GameObject GotoButton;

    void Start()
    {
        Continue(false);
        GotoButton.SetActive(false);
    }


    IEnumerator Type()
    {

        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }
    private void FixedUpdate()
    {
        if (isDiglog)
        {
            if (textDisplay.text == sentences[index])
                Continue(true);
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

     
            if (index < sentences.Length - 1)
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
    public void GoTo()
    {
      
        PlayerPrefs.SetInt("skin"+IdNPC.ToString(), 1);
        StartCoroutine(goTo1());
        
    }
    IEnumerator goTo1() {
        
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + (int)IdNPC);
    }
    
}