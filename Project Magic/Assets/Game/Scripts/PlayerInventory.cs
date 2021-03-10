using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //Inventory example
    private Inventory inventory;

    [SerializeField]
    private UI_Inventory uiIinventory;

    public GameObject UICanvas;
    public Fungus.Flowchart myFlowchart;
    public KeyCode openInventory;

    public void Start()
    {
        inventory = new Inventory(UseItem);
        uiIinventory.SetInventory(inventory);

    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Sword:
                UseSword();
                break;
            case Item.ItemType.Shield:
                UseShield();
                break;
            case Item.ItemType.Armour:
                UseArmour();
                break;
            case Item.ItemType.FirePotion:
                UseFirePotion();
                break;
            case Item.ItemType.HealingPotion:
                UseHealingPotion();
                break;
            case Item.ItemType.Gold:
                UseGold();
                break;
        }
    }

    //These activate when an item is clicked
    private void UseSword()
    {
        //Equip Sword
        inventory.RemoveItem(new Item { itemType = Item.ItemType.Sword, amount = 1 });
    }
    private void UseShield()
    {
        //Equip Shield
        inventory.RemoveItem(new Item { itemType = Item.ItemType.Shield, amount = 1 });
    }
    private void UseArmour()
    {
        //Equip Armour
        inventory.RemoveItem(new Item { itemType = Item.ItemType.Armour, amount = 1 });
    }
    private void UseFirePotion()
    {
        //Set fire
        inventory.RemoveItem(new Item { itemType = Item.ItemType.FirePotion, amount = 1 });
    }
    private void UseHealingPotion()
    {
        //heal player
        inventory.RemoveItem(new Item { itemType = Item.ItemType.HealingPotion, amount = 1 });
    }
    private void UseGold()
    {

        //inventory.RemoveItem(new Item { itemType = Item.ItemType.Gold, amount = 1 });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            //Touching Item
            inventory.AddItem(itemWorld.GetItem());
            myFlowchart.ExecuteBlock("New Block3");
            itemWorld.Despawn();
        }
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(openInventory))
        {
            UICanvas.SetActive(!(UICanvas.active));
        }
    }

}
