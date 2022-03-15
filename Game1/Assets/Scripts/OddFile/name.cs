using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class name : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject InputName;
    public void StoreName()
    {
        string a = InputName.GetComponent<Text>().text;
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetString("NameCharater",a);
    }
}
