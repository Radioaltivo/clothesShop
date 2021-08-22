using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    UIManager uiManager;
    float xMove, yMove;
    
    [Header("Fields")]
    [SerializeField] float speed;
    [SerializeField] int currentMoney, moneyToSpend;
    //clothes owned before arriving at the store
    [SerializeField] ShopItemData ownedShirt, ownedHat, ownedPants;

    [Header("References")]
    [SerializeField] TextMeshPro floatingPriceTMP;    
    [SerializeField] SpriteRenderer shirt, hat, pants;
    
    //reference for table close to the player
    ShopItemData currentTableItemData;
    //keep the references of equipped items for purchasing
    ShopItemData newEquippedShirt, newEquippedHat, newEquippedPants;
    


    // Start is called before the first frame update
    void Start()
    {
        uiManager = UIManager.instance;
        //newEquippedShirt = ownedShirt;
        //newEquippedHat = ownedHat;
        //newEquippedPants = ownedPants;
        EquipNewClothes(ownedShirt);
        EquipNewClothes(ownedHat);
        EquipNewClothes(ownedPants);
        uiManager.UpdateMoney(currentMoney);
    }

    // Update is called once per frame
    void Update()
    {

        WalkAround();
        ActionButton();

    }
    void WalkAround()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(xMove*speed, yMove*speed, 0);
    }

    void ActionButton()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (currentTableItemData != null)
            {
                EquipNewClothes(currentTableItemData);
            }
        }
    }

    //whenever player picks up a new clothing, update cart, update money cost and equip the new clothing.
    //Interacting with the vendor will close the deal, interacting with tables will revert to original clothes.

    void EquipNewClothes(ShopItemData itemData)
    {
        if (itemData.itemType == ShopItemData.ItemType.Shirt)
        {
            if (newEquippedShirt != itemData)
            {
                if (newEquippedShirt != null)
                {
                    moneyToSpend -= newEquippedShirt.itemBaseValue;
                }
                shirt.sprite = itemData.itemSprite;
                newEquippedShirt = itemData;
                moneyToSpend += itemData.itemBaseValue;
            }
            else
            {
                shirt.sprite = ownedShirt.itemSprite;
                newEquippedShirt = null;
                moneyToSpend -= itemData.itemBaseValue;
            }
        }
        else if (itemData.itemType == ShopItemData.ItemType.Pants)
        {
            if (newEquippedPants != itemData)
            {
                if (newEquippedPants != null)
                {
                    moneyToSpend -= newEquippedPants.itemBaseValue;
                }
                pants.sprite = itemData.itemSprite;
                newEquippedPants = itemData;
                moneyToSpend += itemData.itemBaseValue;
            }
            else
            {
                pants.sprite = ownedPants.itemSprite;
                newEquippedPants = null;
                moneyToSpend -= itemData.itemBaseValue;
            }
        }
        else if (itemData.itemType == ShopItemData.ItemType.Hat)
        {
            if (newEquippedHat != itemData)
            {
                if (newEquippedHat != null)
                {
                    moneyToSpend -= newEquippedHat.itemBaseValue;
                }
                hat.sprite = itemData.itemSprite;
                newEquippedHat = itemData;
                moneyToSpend += itemData.itemBaseValue;
            }
            else
            {
                hat.sprite = ownedHat.itemSprite;
                newEquippedHat = null;
                moneyToSpend -= itemData.itemBaseValue;
            }
        }
        uiManager.UpdateMoneyToSpend(moneyToSpend);
    }

    //detect what clothing is close

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentTableItemData = collision.gameObject.GetComponentInParent<ClothesController>().itemData;
        UpdateFloatingPrice(currentTableItemData.itemBaseValue);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentTableItemData = null;
        DisableFloatingPrice();
    }


    public void UpdateFloatingPrice(int amount)
    {
        floatingPriceTMP.gameObject.SetActive(true);
        floatingPriceTMP.text = amount.ToString();
    }

    public void DisableFloatingPrice()
    {
        floatingPriceTMP.gameObject.SetActive(false);
    }
}
