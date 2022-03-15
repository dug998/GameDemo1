using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public GameObject inventory;
 
    public void open()
    {
      
        inventory.SetActive(!inventory.activeInHierarchy);
    }
}
