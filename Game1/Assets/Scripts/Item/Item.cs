using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public float buffhp;
    public float buffmp;
    public float buffDame;
    public float score;
 
    public void die()
    {
        gameObject.SetActive(false);
    }
}
