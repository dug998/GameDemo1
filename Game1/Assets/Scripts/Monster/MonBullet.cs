using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonBullet : MonoBehaviour
{
    public float bulletSpeed;
    public float Dame=1;
    Rigidbody2D myBody;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        //localRotation vòng quay 
        if (transform.localRotation.z >0)
            myBody.AddForce(new Vector2(-1, 0) * bulletSpeed, ForceMode2D.Impulse);
        else
            myBody.AddForce(new Vector2(1, 0) * bulletSpeed, ForceMode2D.Impulse);
    }

    void Start()
    {
        Invoke("destroy", 1);
    }
    void destroy()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "char")
        {
            myBody.velocity = new Vector2(0, 0);

            CharHealth a = collision.gameObject.GetComponent<CharHealth>();
            a.updateHp(-Dame);
            Destroy(gameObject);
        }
    }
}
