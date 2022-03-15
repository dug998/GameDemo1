using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOrUse1 : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform Character;
    private CharHealth charHealth;
    public GameObject item;

    private void Start()
    {
        Character = GameObject.FindGameObjectWithTag("char").GetComponent<Transform>();
        charHealth = GameObject.FindGameObjectWithTag("char").GetComponent<CharHealth>();
    }

    public void SpawnDropItem()
    {
        Vector2 charPos = new Vector2(Character.position.x + 3, Character.position.y);
        Instantiate(item, charPos, Quaternion.identity);

    }
    public void ItemHP()
    {
       


        charHealth.updateHp(item.GetComponent<Item>().buffhp);

        Destroy(gameObject);

    }
    public void ItemMP()
    {
        charHealth.updateMP(item.GetComponent<Item>().buffmp);
        Destroy(gameObject);
    }
    public void ItemDame()
    {
        charHealth.updateDame(item.GetComponent<Item>().buffDame);
        Destroy(gameObject);
    }
}
