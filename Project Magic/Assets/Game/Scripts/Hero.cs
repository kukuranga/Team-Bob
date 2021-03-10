using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    //Inventory example
    private Inventory inventory;

    [SerializeField]
    private UI_Inventory uiIinventory;

    public Fungus.Flowchart myFlowchart;

    public void Awake()
    {
        inventory = new Inventory();
        uiIinventory.SetInventory(inventory);

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

    
}
