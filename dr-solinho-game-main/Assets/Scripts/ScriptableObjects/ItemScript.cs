using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Create Item")]
public class ItemScript : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public string Description;
    public Rarity Rarity;
    public ItemType ItemType;
    public GameObject Item;
    public int Price;
    public int Duration;
    private GameObject m_Dummy;

    
    public int Count { get
        {
            return
                Inventory.ItemList.FindAll(
                    x => x.ID == this.ID).Count;
        } }


    public int ID { get; private set; }

    public void SetItem(Vector3 pos, Transform parent)
    {
        m_Dummy = Instantiate(Item, pos, Quaternion.identity, parent);
        m_Dummy.name = Name;
        m_Dummy.GetComponent<ItemReference>().SetIntance(this);
        m_Dummy.GetComponent<GetItem>().Created();
        m_Dummy.GetComponent<SpriteRenderer>().sprite = Icon;
    }

    private void OnEnable() =>
        ID = this.GetInstanceID();


}
