using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMenu : MonoBehaviour
{
    [SerializeField]
    private ItemReference m_Element = null;
    [SerializeField]
    private GameObject m_Parent = null;

    public static List<ItemReference> InventoryList = new List<ItemReference>();

    public void Start()
    {
        InstantiateElements();
        MouseSensitive.BuyItemUpdateQtd += UpdateElements;
        MouseSensitive.BuyItemUpdateQtd += AddToInvenoryList;

        GetItem.UpdateQtd += UpdateElements;
        GetItem.UpdateQtd += AddToInvenoryList;

        Actions.UpdateQtd += UpdateElements;
        Actions.UpdateQtd += AddToInvenoryList;
        Actions.UpdateQtd += RemoveFromList;
    }

    private void OnDestroy()
    {

        MouseSensitive.BuyItemUpdateQtd -= UpdateElements;
        MouseSensitive.BuyItemUpdateQtd -= AddToInvenoryList;

        GetItem.UpdateQtd -= UpdateElements;
        GetItem.UpdateQtd -= AddToInvenoryList;

        Actions.UpdateQtd -= UpdateElements;
        Actions.UpdateQtd -= AddToInvenoryList;
        Actions.UpdateQtd -= RemoveFromList;
    }

    private void InstantiateElements()
    {
        bool equalExists = false;

        for(int i = 0; i < Inventory.ItemList.Count; i++)
        {

            if(m_Parent.transform.childCount == 0)
            {

                var obj = Instantiate(m_Element, m_Parent.transform);
                obj.GetComponent<ItemReference>().SetValues(Inventory.ItemList[i]);
                InventoryList.Add(obj.GetComponent<ItemReference>());

            }
            else
            {
                foreach(Transform child in m_Parent.transform)
                {
                    if(child.GetComponent<ItemReference>().GetItem().Name == Inventory.ItemList[i].Name)
                    {
                        
                        equalExists = true;
                        break;
                    }
                }

                if(!equalExists)
                {
                    var obj = Instantiate(m_Element, transform);
                    obj.GetComponent<ItemReference>().SetValues(Inventory.ItemList[i]);
                    
                }
                else
                {
                    equalExists = false;
                }
            }

        }

    }

    private void UpdateElements(ItemReference item)
    {
        foreach(Transform child in m_Parent.transform)
        {
            if(child.GetComponent<ItemReference>().GetItem().ID == item.GetItem().ID)
            {
                if(child.GetComponent<ItemReference>().GetItem().Count == 0)
                {
                    Destroy(child.gameObject);
                }
                else
                {
                    child.GetComponent<ItemReference>().UpdateCount();
                }

                return;
            }
        }
        (Instantiate(m_Element, m_Parent.transform) as ItemReference).SetValues(item.GetItem());
    }

    private void AddToInvenoryList(ItemReference item)
    {
        bool equalExists = false;

        for(int i = 0; i < Inventory.ItemList.Count; i++)
        {

            if(InventoryList.Count == 0)
            {
                InventoryList.Add(item);
            }
            else
            {
                foreach(var child in InventoryList)
                {
                    if(child.GetItem().Name == Inventory.ItemList[i].Name)
                    {
                        equalExists = true;
                        break;
                    }
                }

                if(!equalExists)
                {
                    InventoryList.Add(item);
                }
                else
                {
                    equalExists = false;
                }
            }

        }
    }

    private void RemoveFromList(ItemReference item)
    {
        if(item.GetItem().Count == 0)
        {
            InventoryList.Remove(item);
        }
    }


    public void OpenMenu()
    {
        foreach(Transform child in m_Parent.transform)
        {
            if(child.GetComponent<ItemReference>().GetItem().Count == 0)
            {
                Destroy(child.gameObject);
            }
            else
            {
                child.GetComponent<ItemReference>().UpdateCount();
            }
        }
    }


}
