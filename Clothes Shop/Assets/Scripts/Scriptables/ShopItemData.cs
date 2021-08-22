using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TradeItem", order = 1)]
public class ShopItemData : ScriptableObject
{
    public enum ItemType
    {
        Hat,
        Shirt,
        Pants
    }
    public ItemType itemType;
    public int itemBaseValue;
    public Sprite itemSprite;
}