using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharBullet : MonoBehaviour
{
    public float bulletSpeed;
    
    Rigidbody2D myBody;
    private CharHealth CharHealth;
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        //localRotation vòng xoay 
       // kiểm tra trục xoay y của viên đạn để tác động 1 lực giúp viên đạn di chuyển

        if (transform.localRotation.y > 0)
            myBody.AddForce(new Vector2(-1, 0) * bulletSpeed, ForceMode2D.Impulse);
        else
            myBody.AddForce(new Vector2(1, 0) * bulletSpeed, ForceMode2D.Impulse);

        CharHealth = GameObject.FindGameObjectWithTag("char").GetComponent<CharHealth>();
    }

    void Start()
    {
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* viên đạn va chạm quái 
          kiểm tra  đổi hướng cho quái để tấn công nhân vật
          Trừ máu của quái bằng cách lấy dame nhân vật bên CharHealth
      
        */
        if (collision.gameObject.tag == "monster")
        {
          

            MonControl a = collision.gameObject.GetComponent<MonControl>();
            
            if (a.FacingRight() && collision.transform.position.x > transform.position.x)
            {
                a.facing();
            }
            else if (!a.FacingRight() && collision.transform.position.x < transform.position.x)
            {
                a.facing();
            }

            a.updataHealth(-CharHealth.DameChar);
            a.attack(true);

            //if (PlayerPrefs.GetInt("skin2", 0) != 0)
            //    a.attack(false);
            Destroy(gameObject);
        }
    }
   
}
