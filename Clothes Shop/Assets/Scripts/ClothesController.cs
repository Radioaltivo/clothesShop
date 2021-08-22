using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesController : MonoBehaviour
{
    [SerializeField] ShopItemData itemData;
    [SerializeField] SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer.sprite = itemData.itemSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
