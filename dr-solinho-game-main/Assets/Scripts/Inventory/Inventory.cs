using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{

    public static event System.Action<ItemType, bool> HaveSpecialItem;
    public static List<ItemScript> ItemList = new List<ItemScript>();



    private void Awake()
    {
        ItemList = new List<ItemScript>();
        MouseSensitive.IsClicked += RemoveItem;
        MouseSensitive.BuyItemToInventory += AddItem;
        MouseSensitive.PayPrice += RemoveItem;
        GetItem.ToInventory += AddItem;
        Actions.ToInventory += AddItem;
        Actions.Remove += RemoveItem;
    }

    private void OnDestroy()
    {
        MouseSensitive.IsClicked -= RemoveItem;
        MouseSensitive.BuyItemToInventory -= AddItem;
        MouseSensitive.PayPrice -= RemoveItem;
        GetItem.ToInventory -= AddItem;
        Actions.ToInventory -= AddItem;
        Actions.Remove -= RemoveItem;
    }

    public void AddItem(ItemScript item)
    {
        if(item != null)
            ItemList.Add(item);


        if(item.ItemType != ItemType.NORMAL)
            HaveSpecialItem?.Invoke(item.ItemType, true);

    }

    public void RemoveItem(ItemScript item)
    {
        if(item != null)
            ItemList.Remove(item);


        if(item.ItemType != ItemType.NORMAL)
            HaveSpecialItem?.Invoke(item.ItemType, false);
    }

    public static ItemScript GetCoin()
    {
        foreach(ItemScript coin in ItemList)
        {
            if(coin.ItemType == ItemType.COIN)
                return coin;
        }

        return null;
    }

}
