using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update

    private Inventory inventory;
    public GameObject itemButton;
    public ValuesItem valuesItem =ValuesItem.hp;
    public float values;
    // public GameObject effect;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("char").GetComponent<Inventory>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("char"))
        {
            // spawn the sun button at the first available inventory slot ! 


            for (int i = 0; i < inventory.items.Length; i++)
            {
                // kiểm tra xem vị trí có EMPTY không
                if (inventory.items[i]==false)
                {
                    switch(valuesItem)
                    {
                        case ValuesItem.hp:
                            itemButton.GetComponent<Item>().buffhp = values;
                            break;
                        case ValuesItem.mp:
                            itemButton.GetComponent<Item>().buffmp = values;
                            break;
                        case ValuesItem.dame:
                            itemButton.GetComponent<Item>().buffDame = values;
                            break;

                    }    
                
                    inventory.items[i] = true; // đảm bảo rằng vị trí hiện được coi là có vật phẩm
                    Instantiate(itemButton, inventory.slots[i].transform, false); 
                  
                    Destroy(gameObject);
                    break;
                }
            }
        }

    }
    public enum ValuesItem
    {
        hp,
        mp,
        dame
    }

}
