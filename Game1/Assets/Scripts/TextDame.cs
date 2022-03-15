using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextDame : MonoBehaviour
{
    private TextMesh text;
    private Color textColor;
    public float dame;
   
    void Start()
    {
        text = GetComponent<TextMesh>();
        textColor = text.color;
        //  text.color = Color.red;
        Destroy(gameObject,2);

    }
    float disappearTimer=1f;
   
    void Update()
    {

        Vector3 a = transform.localPosition; a.y += 0.01f;transform.localPosition = a;
         textColor.a -=  Time.deltaTime*0.2f;
         text.color = textColor;
        if (dame != 0)
            SetUp(dame.ToString());

        


    }
    public void SetUp(string Dame)
    {
      //  text.SetText(Dame.ToString());
        text.text = Dame;
    }
   

}
