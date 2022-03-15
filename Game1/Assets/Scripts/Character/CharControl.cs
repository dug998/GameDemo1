using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class CharControl : MonoBehaviour
{
    public float maxSpeed;
    public float jumpHeight;
  
    bool facingRight=true;

    bool ground= true;

    Rigidbody2D myBody;


    Animator myAnim;


    //biến bắn
    public Transform GunTip;
    public GameObject bullet;


    float nextFire = 0;
    float fireRate = 1f;


    public GameObject PositionName;
    public GameObject Skins;
    public GameObject Setup;
    SpriteRenderer spriteRenderer;
    private CharHealth CharHealth;
    private FileAudio FileAudio;
   
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
   
        CharHealth = GetComponent<CharHealth>();
        FileAudio = GetComponent<FileAudio>();

        PositionName.GetComponent<TextMesh>().text = PlayerPrefs.GetString("NameCharater","dung");

        Skins.SetActive(false);
        nextFire = Time.time;


    }


    void FixedUpdate()
    {

        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));
        myBody.velocity = new Vector2(move * maxSpeed, myBody.velocity.y);



        if (move > 0 && !facingRight)
        {
            flip();
        }
        else if (move < 0 && facingRight)
        {
            flip();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (ground)
            {
                ground = false;
                 myAnim.SetBool("Jump", true);
                //velocity điều chỉnh vận tốc
                myBody.velocity = new Vector2(myBody.velocity.x, jumpHeight);

                FileAudio.Playsound("jump");

            }
        }
        ////chức năng đánh GetAxisRaw chuyen luon 1->0
        ////Input.GetAxisRaw("Fire1") > 0

       // bấm A hiện skin đang có
        if (Input.GetKey(KeyCode.A))
        {
            Skins.SetActive(!Skins.activeInHierarchy);
        }

        // bấm S hiện lựa chọn Pause và RePlay
        if (Input.GetKey(KeyCode.S))
        {
            Setup.SetActive(!Setup.activeInHierarchy);
        }

        // tấn công
        if (Time.time > nextFire)
        {
            nextFire += fireRate;
            if (Input.GetAxisRaw("Fire2") > 0)
            {
                myAnim.SetBool("attack", true);
                fireBullet();
            }

            else
                myAnim.SetBool("attack", false);
        }
    } 
    
    void fireBullet()
    {   //chức năng bắn đạn 
        // -1 Mp của nhân vật
        
        if (CharHealth.OnAttack)
        {
           
            if (facingRight)
            {

                Instantiate(bullet, GunTip.position, Quaternion.Euler(new Vector3(0, 0, 0)));

            }
            else if (!facingRight)
            {

                Instantiate(bullet, GunTip.position, Quaternion.Euler(new Vector3(0, 180, 0)));
            }
            CharHealth.updateMP(-1);
        }

    }
   
    void flip()
    {  
        //Quay ngược hướng mặt nhân vật ;
        facingRight = !facingRight;
        spriteRenderer.flipX= ! spriteRenderer.flipX;
      
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // bắt va chạm đầu tiên mặt đất  
        if (collision.gameObject.tag == "Ground")
        {

            myAnim.SetBool("Jump", false);

            ground = true;
            if (transform.position.y < collision.gameObject.transform.position.y)
            {
                collision.collider.isTrigger = true;

            }
        }


    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        // bắt va chạm cuối cùng nhân vật với mặt đất 
        if (collision.gameObject.tag == "Ground")
        {
            if (transform.position.y >= collision.gameObject.transform.position.y)
            {

                Debug.Log("1");
                collision.isTrigger = false;


            }

        }
      

        // khi nhân vật va chạm lần cuối cùng NPC ẩn  hộp thoại và nút Continue 
        if (collision.gameObject.tag == "NPC")
        {

            DiaLog a = collision.gameObject.GetComponent<DiaLog>();
            a.isDiglog = false;

            a.Continue(false);
          
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // khi nhân vật va chạm lần đầu cùng NPC hiển  hộp thoại và nút Continue 

        if (collision.gameObject.tag == "NPC")
        {

            DiaLog a = collision.gameObject.GetComponent<DiaLog>();
            a.isDiglog = true;
            a.Continue(true);
           
        }

        // hoàn thành nhiệm vụ kết thúc game tại NPCEndGame
        if (collision.gameObject.tag == "NPCEndGame")
        {

            DiaLogEndGame a = collision.gameObject.GetComponent<DiaLogEndGame>();
            a.isDiglog = true;
            a.Continue(true);


        }

        // khi nhân vật nhặt được ngọc rồng lưu lại viên ngọc và chuyển về bản đồ Home
        if (collision.gameObject.tag == "NgocRong")
        {

            NgocRong a = collision.gameObject.GetComponent<NgocRong>();
            PlayerPrefs.SetInt("NgocRong"+a.IdNgocRong,1);
            GotoHome();
            
        }


    }
    void GotoHome()
    {
        StartCoroutine(GotoHome1());
    }
    IEnumerator GotoHome1()
    {

        yield return new WaitForSeconds(3f);
       
        SceneManager.LoadScene(1);
        
    }
}
