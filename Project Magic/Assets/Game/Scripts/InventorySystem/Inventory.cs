using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public event EventHandler onItemListChanged;
    private List<Item> itemList;
  

    public Inventory()
    {
        itemList = new List<Item>();

        //test
        //AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.Helmet, amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.pants, amount = 1 });
        //Debug.Log("ItemsStored");
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemIsInInventory = false;
            foreach(Item invItem in itemList)
            {
                if(invItem.itemType == item.itemType)
                {
                    invItem.amount += item.amount;
                    itemIsInInventory = true;
                }
            }
            if(!itemIsInInventory)
            {
                itemList.Add(item);
            }
        }
        else
            itemList.Add(item);

        onItemListChanged?.Invoke(this, EventArgs.Empty);
        Debug.Log("Item Collected");
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
