using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TradeItem", order = 1)]
public class ShopItemData : ScriptableObject
{
    public string itemName;
    public int itemBaseValue;
    public int itemBuyValue;
    public int itemSellValue;
   
}