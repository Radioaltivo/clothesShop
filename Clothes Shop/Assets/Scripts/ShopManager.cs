using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    [SerializeField] List<ClothesController> clothesList = new List<ClothesController>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
    }

    public void DestroyClothesSold(ShopItemData itemData)
    {
        foreach (ClothesController item in clothesList)
        {
            if (item.itemData == itemData)
            {
                item.gameObject.SetActive(false);
                return;                
            }
        }
    }
    
}