using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemsAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }


    private Item item;
    private SpriteRenderer spriteRenderer;

    private TextMeshPro textMesh;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMesh = transform.Find("text").GetComponent<TextMeshPro>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();

        if(item.amount > 1)
            textMesh.SetText(item.amount.ToString());
        else
            textMesh.SetText("");
    }


    public Item GetItem()
    {
        return item;
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
