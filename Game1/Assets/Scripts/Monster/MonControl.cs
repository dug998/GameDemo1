using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MonControl : MonoBehaviour
{
    public float maxHealth;

    float presentHealth;
    public int IdMonter=0;
    public GameObject FloatingDame;
    public GameObject bullet;
    public GameObject item;
    public GameObject effectSkin;


    public GameObject deathEffect;
    public Scrollbar enemyHealthScrollbar;

    Animator myAnim;

    bool facingRight = true;
    bool OnAttack = false;

    float Direction = 1;
    float maxLeft;
    float maxRight;
    Rigidbody2D myBody;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        presentHealth = maxHealth;
        myAnim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //  myAnim = GetComponent<Animator>();
        Direction = 1;
        maxLeft = transform.position.x - 2;
        maxRight = transform.position.x + 2;

        enemyHealthScrollbar.size = presentHealth/maxHealth;

        transform.GetChild(0).gameObject.SetActive(false);

    }

    float nextTime = 0;
   
    // Update is called once per frame
   
    private void FixedUpdate()
    {
        // khi quái bị tấn công onAttack =true thì quái tấn công lại với delay 3s 
        //khi  nhân vật tấn công có skin 2 hỗ trợ quái sẽ bị làm chậm   delay 4s
        if (IdMonter == 1)
        {
            float present = transform.position.x;

            if (present >= maxRight)
                Direction = -1;
            else if (present <= maxLeft)
            {
                Direction = 1;
            }
            spriteRenderer.flipX = Direction==1?true:false;
            myBody.velocity = new Vector2(Direction * 1, myBody.velocity.y);
        }
        float Delay = 3.0f;
        if (Time.time > nextTime)
            if (OnAttack)
            {
                nextTime = Time.time + Delay;

                // nếu có chiêu 2 thì quái bị đóng băng dẫn đến làm chậm lần đâu tiên 
                if (PlayerPrefs.GetInt("skin2", 0) != 0)
                {
                    Delay = 4.0f;
                    Instantiate(effectSkin, transform.position, Quaternion.identity);
                    PlayerPrefs.SetInt("skin2", 0);
                    return;


                }
                else
                {
                    if (IdMonter == 0)
                    {
                        if (facingRight)
                            Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        else if (!facingRight)
                            Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                    }
                    else if (IdMonter == 1)
                    {
                        SpawnProjectiles(numberofProjectiles);

                    }
                }

                nextTime = Time.time + Delay;

            }


    }
    int numberofProjectiles=10;
    float radius = 5;
            void SpawnProjectiles(int numberofprojectiles)
            {
                int numberofProjectiles = 10;
                float radius = 5;
                float anglestep =-90f / numberofProjectiles;
                float angle =0f;
                for (int i = 0; i <= numberofProjectiles - 1; i++)
                {
                    float projectileDirxposition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                    float projectileDiryposition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) *radius;

                    Vector2 projectilevector = new Vector2(projectileDirxposition, projectileDiryposition);
                    Vector2 projectileMoveDirection = (projectilevector - (Vector2)transform.position).normalized * 10;
                    var proj =Instantiate(bullet, transform.position, Quaternion.identity);
                    proj.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
                    angle += anglestep;
            
        } 
            }
    public void updataHealth(float dame)

    {
        // hiện thị dame quái nhận , cập nhật lại thể lực hiện tại của quái và hiện lại thanh máu
        presentHealth += dame;
        
        FloatingDame.GetComponent<TextDame>().dame=dame;
        Instantiate(FloatingDame,transform.position, Quaternion.identity);
       
        enemyHealthScrollbar.size = presentHealth / maxHealth;

        if (presentHealth <= 0)
        {
            die();
        }
    }
    void die()
    {
        // tạo hiệu ứng chết + rớt vật phẩm 
        Instantiate(deathEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
        PlayerPrefs.SetInt("skin1", 1);
        Instantiate(item, transform.position, transform.rotation);
        
    }

    public void attack(bool x)
    {
     // khi quái tấn công hiển thị thanh máu
        myAnim.SetBool("MonsterAttack", x);
        OnAttack = true;
       
        transform.GetChild(0).gameObject.SetActive(true);

    }
   
    public void facing()
    {
        facingRight = !facingRight;
      
        spriteRenderer.flipX = !spriteRenderer.flipX;

    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* khi nhân vật chạm vào quái => -1 máu của nhân vật
         xác định vị trí nhân vật thay đổi hướng quái cho phù hợp => kích hoạt tấn công
        */
        if (collision.gameObject.tag == "char")
        {

            CharHealth a = collision.gameObject.GetComponent<CharHealth>();

            a.updateHp(-1);


            if (facingRight && collision.transform.position.x > transform.position.x)
            {
                facing();
            }
            else if (!facingRight && collision.transform.position.x < transform.position.x)
            {
                facing();
            }
            attack(true);
        }
    }
    public bool FacingRight()
    {
        return facingRight;
    }
}
