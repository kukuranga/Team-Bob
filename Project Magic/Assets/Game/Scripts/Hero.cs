using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    //Inventory example
    private Inventory inventory;

    [SerializeField]
    private UI_Inventory uiIinventory;

    public void Awake()
    {
        inventory = new Inventory();
        uiIinventory.SetInventory(inventory);

        //testing
        //ItemWorld.SpawnItemWorld(new Vector3(5, 0), new Item { itemType = Item.ItemType.Sword , amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(-5, 0), new Item { itemType = Item.ItemType.boots, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(5, -2), new Item { itemType = Item.ItemType.Helmet, amount = 1 });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            //Touching Item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.Despawn();
        }
        
    }
}
