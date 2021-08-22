using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;
    float xMove, yMove;
    [SerializeField] SpriteRenderer shirt, hat, pants;
    ShopItemData currentTableItemData;

    //keep the references of equipped items for later
    ShopItemData currentEquippedShirt, currentEquippedHat, currentEquippedPants;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        WalkAround();

        if (Input.GetButtonDown("Jump"))
        {
            if (currentTableItemData != null)
            {
                EquipNewClothes(currentTableItemData);
            }

        }
    }
    void WalkAround()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(xMove*speed, yMove*speed, 0);
    }


    void EquipNewClothes(ShopItemData itemData)
    {

        if (itemData.itemType == ShopItemData.ItemType.Shirt)
        {
            if (currentEquippedShirt != itemData)
            {
                shirt.sprite = itemData.itemSprite;
                currentEquippedShirt = itemData;
            }
        }
        else if (itemData.itemType == ShopItemData.ItemType.Pants)
        {
            if (currentEquippedPants != itemData)
            {
                pants.sprite = itemData.itemSprite;
                currentEquippedPants = itemData;
            }
        }
        else if (itemData.itemType == ShopItemData.ItemType.Hat)
        {
            if (currentEquippedHat != itemData)
            {
                hat.sprite = itemData.itemSprite;
                currentEquippedHat = itemData;
            }
        }
    }

    //detect what clothing is close

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentTableItemData = collision.gameObject.GetComponentInParent<ClothesController>().itemData;
        print(currentTableItemData);
    }

}
