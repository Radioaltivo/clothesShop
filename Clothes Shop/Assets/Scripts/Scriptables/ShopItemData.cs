using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TradeItem", order = 1)]
public class ShopItemData : ScriptableObject
{
    public enum ItemType
    {
        Hat,
        Chest,
        Pants
    }
    public int itemBaseValue;
    public Sprite itemSprite;
}