using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //Inventory example
    private Inventory inventory;

    [SerializeField]
    private UI_Inventory uiIinventory;
    private PlayerController pc;

    public bool hasSword;
    public bool hasShield;
    public bool hasArmour;

    public GameObject UICanvas;
    public Fungus.Flowchart blackSmithFlowchart;
    public Fungus.Flowchart WarriorFlowchart;
    public Fungus.Flowchart exitGuardFlowchart;
    public KeyCode openInventory;

    public void Start()
    {
        inventory = new Inventory(UseItem);
        uiIinventory.SetInventory(inventory);
        pc = GetComponent<PlayerController>();
        hasSword = false;
        hasShield = false;
        hasArmour = false;

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
                UseGold(item.amount);
                break;
        }
    }

    //These activate when an item is clicked
    private void UseSword()
    {
        //Equip Sword
        pc.AddAttack(100);
        hasSword = true;
        //inventory.RemoveItem(new Item { itemType = Item.ItemType.Sword, amount = 1 });
    }
    private void UseShield()
    {
        //Equip Shield
        pc.AddArmour(50);
        hasShield = true;
        //inventory.RemoveItem(new Item { itemType = Item.ItemType.Shield, amount = 1 });
    }
    private void UseArmour()
    {
        //Equip Armour
        pc.AddArmour(100);
        hasArmour = true;
        //inventory.RemoveItem(new Item { itemType = Item.ItemType.Armour, amount = 1 });
    }
    private void UseFirePotion()
    {
        //Set fire
        //pc.heal(-50);
        //inventory.RemoveItem(new Item { itemType = Item.ItemType.FirePotion, amount = 1 });
    }
    private void UseHealingPotion()
    {
        //heal player
        //pc.heal(50);
        //inventory.RemoveItem(new Item { itemType = Item.ItemType.HealingPotion, amount = 1 });
    }
    private void UseGold(int num)
    {
        pc.AddCoin(num);
        inventory.RemoveItem(new Item { itemType = Item.ItemType.Gold, amount = num });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            //Touching Item
            AddUseItem(itemWorld.GetItem());
            //myFlowchart.ExecuteBlock("New Block3");
            itemWorld.Despawn();
        }
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(openInventory))
        {
            UICanvas.SetActive(!(UICanvas.active));
        }
        flowchartLogic();
    }
    private void flowchartLogic()
    {
        if (hasSword)
        {
            WarriorFlowchart.ExecuteBlock("New Block3");
            exitGuardFlowchart.ExecuteBlock("SwordCheck");
            hasSword = false;
        }
        if (hasShield)
        {
            WarriorFlowchart.ExecuteBlock("New Block3");
            exitGuardFlowchart.ExecuteBlock("ShieldCheck");
            hasShield = false;
        }
        if(hasArmour)
        {
            blackSmithFlowchart.ExecuteBlock("New Block3");
            exitGuardFlowchart.ExecuteBlock("ArmourCheck");
            hasArmour = false;
        }

    }
    private void AddUseItem(Item item)
    {
        inventory.AddItem(item);
        UseItem(item);
    }
}
