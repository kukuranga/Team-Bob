using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsAssets : MonoBehaviour
{
    
    public static ItemsAssets Instance { get; private set; }

    private void Awake()
    { 
        Instance = this;        
    }

    public Transform pfItemWorld;

    public Sprite SwordSprite;
    public Sprite ShieldSprite;
    public Sprite HealingPotionSprite;
    public Sprite FirePotionSprite;
    public Sprite GoldSprite;
    public Sprite ArmourSprite;
}
