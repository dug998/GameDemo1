using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class slot : MonoBehaviour
{
    // Start is called before the first frame update
    private Inventory inventory;
    public GameObject RemoveItem;
    public int index;

    private void Start()
    {

        inventory = GameObject.FindGameObjectWithTag("char").GetComponent<Inventory>();

    }

    
    private void FixedUpdate()
    {
        //kiểm tra trong chỗ của hành trang
        // nếu có vật phẩm hiện nút xóa item <=> ẩn
        if (transform.childCount <= 0)
        {
            inventory.items[index] = false;
            RemoveItem.SetActive(false);
        }
        else
        {
           RemoveItem.SetActive(true);
        }
    }
    public void DropItem()
    {

        foreach (Transform child in transform)
        {
  
            GameObject.Destroy(child.gameObject);
        }
    }
  
}
