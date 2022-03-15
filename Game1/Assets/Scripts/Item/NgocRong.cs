using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NgocRong : MonoBehaviour
{
    public float IdNgocRong;
    public float TimeExist;
   
    void Start()
    {
        Destroy(gameObject, TimeExist);
    }

 
}
