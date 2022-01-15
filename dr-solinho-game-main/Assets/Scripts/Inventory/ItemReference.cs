using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemReference : MonoBehaviour
{
    public Image Icon;
    public Text CountText;
    public GameObject Item;    
    [SerializeField]
    private ItemScript m_ItemScript;

    private Rarity m_Rarity;
    private Outline m_Outline;

    public void SetValues(ItemScript item)
    {
        m_ItemScript = item;
        m_Rarity = item.Rarity;
        Icon.sprite = item.Icon;
        Item = item.Item;
        m_Outline = null;
        SetChild();
        UpdateCount();        
    }

    public void SetIntance(ItemScript item)
    {
        m_ItemScript = item;
        Item = item.Item;
    }

    public void UpdateCount() => 
        CountText.text = "x" + m_ItemScript.Count.ToString();
    

    public ItemScript GetItem() => 
        m_ItemScript;
    

    private void SetChild()
    {
        if(transform.childCount > 0)
        {
            foreach(Transform child in transform)
            {
                if(child.CompareTag("Rarity"))
                {
                    m_Outline = child.GetComponent<Outline>();
                }
            }

            if(m_Rarity == Rarity.EXOTIC)
            {
                m_Outline.effectColor = Color.red;
            }
            else if(m_Rarity == Rarity.RARE)
            {
                m_Outline.effectColor = Color.blue;
            }
            else if(m_ItemScript.ItemType == ItemType.COIN)
            {
                m_Outline.effectColor = Color.yellow;
            }
        }
    }
}
