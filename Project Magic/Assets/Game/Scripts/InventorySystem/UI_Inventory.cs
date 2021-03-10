using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    public int SpaceBetweenItemSlot;
    public GameObject ItemsTemplate;
    public GameObject ISlotTemplate;
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Awake()
    {
        //itemSlotContainer = transform.Find("ItemsTemplate");
        //itemSlotTemplate = itemSlotContainer.Find("itemsSlotTemplate");
        itemSlotContainer = ItemsTemplate.transform;
        itemSlotTemplate = ISlotTemplate.transform;
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.onItemListChanged += Inventory_onItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_onItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    //locates all item slots in grid array
    private void RefreshInventoryItems()
    {
        
        foreach(Transform t in itemSlotContainer)
        {
            if (t == itemSlotTemplate)
                continue;
            Destroy(t.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = SpaceBetweenItemSlot + 50;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI text = itemSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                text.SetText(item.amount.ToString());
            }
            else
            {
                text.SetText("");
            }

            x++;
            if(x > 4)
            {
                x = 0;
                y++;
            }
        }
    }

}
