using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CharHealth : MonoBehaviour
{
   // public float maxHealth;
    // sức khỏe và ma na
    public float maxHP;
    public float maxMP;
    public float maxLife = 3;
    
    public Scrollbar healthSliderHp;
    public Scrollbar healthSliderMp;

    float presentHp;
    float presentMp;
    float life;
    float Score;
    public float DameChar=1;

    public  TextMeshProUGUI[] info;

    //public float maxScore;
    //float Score = 0;

    // public GameObject GameOver;



    public GameObject GameOver;
    
    public bool OnAttack = true;
   

    //nhận

    void Start()
    {  // máp 1 không thể đánh được
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            OnAttack = false;
        }

       // nếu có skin hỗ trợ

        if (PlayerPrefs.GetInt("skin1", 0) != 0)
            maxHP *= 10;
        if (PlayerPrefs.GetInt("skin3", 0) != 0)
            DameChar *= 10;
        // gán giá trị

        life = maxLife;
        presentHp = maxHP;
        presentMp = maxMP;

        //hiện thị thông tin nhân vật 

        info[0].text = "HP :" + presentHp + " / " + maxHP;
        info[1].text = "MP :" + presentMp + " / " + maxMP;
        info[2].text = "Dame :" + +DameChar;
        info[3].text = "Life :" + PlayerPrefs.GetInt("Life",0) + " / " + maxLife;
        info[4].text = "Score : "+ PlayerPrefs.GetFloat("Score", 0);

        // thanh hiện thị 
        healthSliderHp.size = 1;

        healthSliderMp.size = 1;
       



    }
    private void FixedUpdate()
    {// cập nhật thông số liên tục cho nhân vật hiện thị trong inventory
      
        info[0].text = "HP :" + presentHp + " / " + maxHP;
        info[1].text = "MP :" + presentMp + " / " + maxMP;
        info[2].text = "Dame :" + +DameChar;
        info[3].text = "Life :" + PlayerPrefs.GetInt("Life", 0) + " / " + maxLife;
        info[4].text = "Score : "+ PlayerPrefs.GetFloat("Score", 0);

        //nếu Mp hiện tại < 0 nhân vật không thể bắn ra viên đạn
        if (presentMp <= 0)
            OnAttack = false;
        else
            OnAttack = true;
    }



    public void updateHp(float dame)
    {

        // nếu nhân vật đang có chiêu hỗ trợ 4 nhân vật ko bị mất máu
        if (PlayerPrefs.GetInt("skin4", 0) != 0 && dame < 0)
              return;

            presentHp += dame;
        if (presentHp > maxHP)
            presentHp = maxHP;
        healthSliderHp.size = presentHp/maxHP;
        if (presentHp <= 0)
            die();

    }
    public void updateMP(float x)
    {
        

        presentMp += x;
        if (presentMp > maxMP)
            presentMp = maxMP;
        healthSliderMp.size = presentMp / maxMP;
        if (presentMp <= 0)
            OnAttack = false;
       
    }
    public void updateScore(float score)
    {

        PlayerPrefs.SetFloat("Score", score);
    }
    public void updateDame(float x)
    {
        // giới hạn sức tấn công của nhân vật
        DameChar += x;
        if (DameChar > 100)
        {
            DameChar = 100;
        }
    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {

        

        if (collision.gameObject.tag == "score")
        {
            // tăng score khi nhặt item
            Item item = collision.gameObject.GetComponent<Item>();
            updateScore(item.score);
            item.die();
        }
        if (collision.gameObject.tag == "DeathZone")
        {
            
            die();

        }
        

    }

    void die()
    {/*
      kiểm tra xem nhân vật đã hết số lần số chưa
     -) nếu chết 
        hiện game over
        cập nhật lại life và Score
     -) không thì
        mạng sống bị giảm 1
        gán lại Hp và Mp 
        về lại việt nam
      */
        if (PlayerPrefs.GetInt("Life", 0) <= 0)
        {
            gameObject.SetActive(false);
            GameOver.SetActive(true);
            PlayerPrefs.SetInt("Life", 3);
            PlayerPrefs.SetFloat("Score", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Life", PlayerPrefs.GetInt("Life", 0) - 1);
            presentHp = maxHP;
            presentMp = maxMP;
            SceneManager.LoadScene(1);
        }
    
    }
}
