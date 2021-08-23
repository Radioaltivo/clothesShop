using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    UIManager uiManager;
    ShopManager shopManager;
    float xMove, yMove;
    
    [Header("Fields")]
    [SerializeField] float speed;
    [SerializeField] int currentMoney, moneyToSpend;
    //clothes owned before arriving at the store
    [SerializeField] ShopItemData ownedShirt, ownedHat, ownedPants;
    [SerializeField] Color floatingPriceYellow, floatingPriceRed;

    [Header("References")]
    [SerializeField] TextMeshPro floatingPriceTMP;    
    [SerializeField] SpriteRenderer shirt, hat, pants;
    
    //reference for table close to the player
    ShopItemData currentTableItemData;
    //keep the references of equipped items for purchasing
    ShopItemData newEquippedShirt, newEquippedHat, newEquippedPants;

    bool isTalkingToShopkeeper = false;
    bool inputsEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = UIManager.instance;
        shopManager = ShopManager.instance;
        EquipNewClothes(ownedShirt);
        EquipNewClothes(ownedHat);
        EquipNewClothes(ownedPants);
        uiManager.UpdateMoney(currentMoney);
    }

    // Update is called once per frame
    void Update()
    {

        
        DetectInputs();

    }

    private void FixedUpdate()
    {
        WalkAround();
    }
    void WalkAround()
    {

        transform.position += new Vector3(xMove * speed * Time.deltaTime, yMove * speed*Time.deltaTime, 0);
    }

    void DetectInputs()
    {
        if (inputsEnabled)
        {
            xMove = Input.GetAxisRaw("Horizontal");
            yMove = Input.GetAxisRaw("Vertical");
            if (Input.GetButtonDown("Jump"))
            {
                if (isTalkingToShopkeeper)
                {
                    PurchaseItems();
                }
                else if (currentTableItemData != null)
                {
                    EquipNewClothes(currentTableItemData);
                }
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
                if (moneyToSpend + itemData.itemBaseValue <= currentMoney)
                {
                    SetFloatingPriceColor(floatingPriceYellow);
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
                    SetFloatingPriceColor(floatingPriceRed);
                }
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
                if (moneyToSpend + itemData.itemBaseValue <= currentMoney)
                {
                    SetFloatingPriceColor(floatingPriceYellow);
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
                    SetFloatingPriceColor(floatingPriceRed);
                }
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
                if (moneyToSpend + itemData.itemBaseValue <= currentMoney)
                {
                    SetFloatingPriceColor(floatingPriceYellow);
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
                    SetFloatingPriceColor(floatingPriceRed);
                }
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
        if (collision.CompareTag("Clothes"))
        {

            currentTableItemData = collision.gameObject.GetComponentInParent<ClothesController>().itemData;
            UpdateFloatingPrice(currentTableItemData.itemBaseValue);
        }
        else if (collision.CompareTag("Shopkeeper"))
        {
            isTalkingToShopkeeper = true;
        }
        else if (collision.CompareTag("Door"))
        {
            uiManager.OpenEndgamePanel();
            ToggleInputs(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Clothes"))
        {

            currentTableItemData = null;
            DisableFloatingPrice();
        }
        else if (collision.CompareTag("Shopkeeper"))
        {
            isTalkingToShopkeeper = false;
        }
    }


    public void UpdateFloatingPrice(int amount)
    {       

        floatingPriceTMP.gameObject.SetActive(true);
        floatingPriceTMP.text = amount.ToString();
    }

    void SetFloatingPriceColor(Color color)
    {
        floatingPriceTMP.color = color;
    }

    public void DisableFloatingPrice()
    {
        floatingPriceTMP.gameObject.SetActive(false);
    }

    void PurchaseItems()
    {
        currentMoney -= moneyToSpend;
        moneyToSpend = 0;
        uiManager.UpdateMoney(currentMoney);
        uiManager.UpdateMoneyToSpend(moneyToSpend);

        if (newEquippedHat != null)
        {
            ownedHat = newEquippedHat;
            newEquippedHat.itemBaseValue = 0;
            shopManager.DestroyClothesSold(newEquippedHat);
        }
        if (newEquippedPants != null)
        {
            ownedPants = newEquippedPants;
            newEquippedPants.itemBaseValue = 0;
            shopManager.DestroyClothesSold(newEquippedPants);
        }
        if (newEquippedShirt != null)
        {
            ownedShirt = newEquippedShirt;
            newEquippedShirt.itemBaseValue = 0;
            shopManager.DestroyClothesSold(newEquippedShirt);
        }


    }

    public void ToggleInputs(bool enable)
    {
        inputsEnabled = enable;
    }
}
