using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSkinGame : MonoBehaviour
{
    // Start is called before the first frame update
   
    private void Awake()
    {
        PlayerPrefs.SetInt("skin1", 0);
        PlayerPrefs.SetInt("skin2", 0);
        PlayerPrefs.SetInt("skin3", 0);
        PlayerPrefs.SetInt("skin4", 0);

        PlayerPrefs.SetInt("Life", 3);
        
    }
   
}
