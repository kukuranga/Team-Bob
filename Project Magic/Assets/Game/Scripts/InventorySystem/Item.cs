using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{ 
    public enum ItemType
    {
        Sword,
        Shield,
        HealingPotion,
        FirePotion,
        Gold,
        Armour,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.Sword: return ItemsAssets.Instance.SwordSprite;
            case ItemType.Gold: return ItemsAssets.Instance.GoldSprite;
            case ItemType.FirePotion: return ItemsAssets.Instance.FirePotionSprite;
            case ItemType.HealingPotion: return ItemsAssets.Instance.HealingPotionSprite;
            case ItemType.Shield: return ItemsAssets.Instance.ShieldSprite;
            case ItemType.Armour: return ItemsAssets.Instance.ArmourSprite;

        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:
            case ItemType.Shield:
            case ItemType.Armour:
                return false;
            case ItemType.HealingPotion:
            case ItemType.Gold:
            case ItemType.FirePotion:
                return true;
        }
    }
}
