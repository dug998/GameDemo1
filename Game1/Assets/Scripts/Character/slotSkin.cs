using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotSkin : MonoBehaviour
{
    public GameObject[] skin;

    public GameObject[] effectSkin;


    private void Start()
    {
    
        if (PlayerPrefs.GetInt("skin1", 0) != 0)
        {

            skin[0].SetActive(true);
            effectSkin[0].SetActive(true);
           
        }
        if (PlayerPrefs.GetInt("skin2", 0) != 0)
        {

            skin[1].SetActive(true);
        }
        if (PlayerPrefs.GetInt("skin3", 0) != 0)
        {

            skin[2].SetActive(true);
        }
        if (PlayerPrefs.GetInt("skin4", 0) != 0)
        {

            skin[3].SetActive(true);
        }
    }
    public void UseSkin1()
    {
        
    }
}
