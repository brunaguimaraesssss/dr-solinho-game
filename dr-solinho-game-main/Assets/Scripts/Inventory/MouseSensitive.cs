using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseSensitive : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static event System.Action<string, string, string, string> MouseOn;
    public static event System.Action MouseOFF;
    public static event System.Action<ItemScript> IsClicked;
    public static event System.Action<ItemReference> BuyItemUpdateQtd;
    public static event System.Action<ItemScript> BuyItemToInventory;
    public static event System.Action<ItemScript> PayPrice;
    public static event System.Action<bool> UpdateCoin;
    public static event System.Action<MouseSensitive> SetPanel;

    private MouseSensitive m_Script;

    private GameObject m_Inventory;
    private GameObject m_Player;

    private ItemReference m_Reference;
    private InformationManager m_InfoManager;

    private Vector3 m_ScreenPoint; 
    private Vector3 m_Offset;

    private Vector3 m_PreviousPosition;
    private Vector3 m_NewPosition;
    private bool m_IsHit;
    private bool m_IsSelected;
    private GameObject m_Dummy;

    private BoxCollider m_Table;

    private bool m_IsVendorItem=false;


    void Start()
    {   
        m_Table = GameObject.Find("Scroll Area").GetComponent<BoxCollider>();
        m_Script = GetComponent<MouseSensitive>();
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Inventory = GameObject.FindGameObjectWithTag("Inventory");
        m_InfoManager = FindObjectOfType(typeof(InformationManager)) as InformationManager;
        m_Reference = GetComponent<ItemReference>();
        ChangeColor(Color.gray);
    }


    public void OnPointerClick(PointerEventData eventData)=>
        SetPanel?.Invoke(m_Script);


    private void OnMouseDown()
    {
        if(!m_IsVendorItem)
        {
            m_PreviousPosition = transform.position;
            m_IsSelected = true;

            m_ScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
            m_Offset = transform.position - Camera.main.ScreenToWorldPoint(
                                            new Vector3(Input.mousePosition.x,
                                                        Input.mousePosition.y,
                                                        m_ScreenPoint.z));
        }
    }

    private void OnMouseDrag()
    {
        if(!m_IsVendorItem)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_ScreenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + m_Offset;
            transform.position = curPosition;
        }
    }

    private void OnMouseUp()
    {
        if(!m_IsVendorItem)
        {
            m_IsSelected = false;

            if(m_Table.bounds.Contains(transform.position))
            {
                if(m_IsHit)
                {
                    transform.position = m_NewPosition;
                    m_Dummy.transform.position = m_PreviousPosition;
                    m_IsHit = false;
                }
                else
                    transform.position = m_PreviousPosition;
            }
            else
            {
                SetPanel?.Invoke(m_Script);
                transform.position = m_PreviousPosition;
            }
        }


    }


    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("ItemMenu") && m_IsSelected)
        {
            m_IsHit = true;
            m_NewPosition = collision.transform.position;
            m_Dummy = collision.gameObject;
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeColor(Color.white);
        MouseOn?.Invoke(m_Reference.GetItem().Name, 
            m_Reference.GetItem().Description,
            m_Reference.GetItem().Count.ToString("00"),
            m_Reference.GetItem().Price.ToString("00"));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeColor(Color.gray);
        MouseOFF?.Invoke();
    }

    public void RemoveItem()
    {
        IsClicked?.Invoke(m_Reference.GetItem());
        
        m_Reference.UpdateCount();
        m_Reference.GetItem().SetItem(m_Player.transform.position, m_Inventory.transform);
        
        if(m_Reference.GetItem().Count == 0)
        {
            Destroy(this.gameObject);
            m_InfoManager.HideInformation();
        }
    }

    public void BuyItem()
    {
        if(Inventory.GetCoin() != null)
        {
            if(Inventory.GetCoin().Count >= m_Reference.GetItem().Price)
            {
                BuyItemUpdateQtd?.Invoke(m_Reference);
                BuyItemToInventory?.Invoke(m_Reference.GetItem());
                for(int i=0; i< m_Reference.GetItem().Price; i++)
                {
                    PayPrice?.Invoke(Inventory.GetCoin());
                }
                UpdateCoin?.Invoke(true);
                return;
            }
            UpdateCoin?.Invoke(false);
            return;
        }
    }


    private void OnTriggerExit(Collider collision) =>
        m_IsHit = false;    

    private void ChangeColor(Color color) =>
        m_Reference.Icon.color = color;

    public void SetVendorItem(bool isvendor) =>
        m_IsVendorItem = isvendor;

    public bool GetVendorItem() =>
        m_IsVendorItem;
    
}
