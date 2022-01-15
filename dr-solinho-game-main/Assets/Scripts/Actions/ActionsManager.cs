using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionsManager : MonoBehaviour
{

    public static event System.Action<string> SetAction;
    public static event System.Action StopAction;
    public static event System.Action<bool> SetHold;

    private bool m_IsActive;
    [SerializeField]
    private GameObject m_CancelActionButton=null;
    [SerializeField]
    private GameObject m_ActionsPanelList = null;
    [SerializeField]
    private GameObject m_OpenActionsButton = null;

    [SerializeField]
    private GameObject m_DigActionButton = null;
    [SerializeField]
    private GameObject m_GetWaterActionButton = null;
    [SerializeField]
    private GameObject m_PutOutFireActionButton = null;
    [SerializeField]
    private GameObject m_WateringActionButton = null;


    private void Start()
    {
        Inventory.HaveSpecialItem += SetButton;
    }

    private void OnDestroy()
    {
        Inventory.HaveSpecialItem -= SetButton;
    }

    private void SetButton(ItemType type, bool set)
    {
        switch(type)
        {
            case ItemType.BUCKET:
                m_GetWaterActionButton.SetActive(set);
                m_PutOutFireActionButton.SetActive(set);
                break;
            case ItemType.SHOVEL:
                m_DigActionButton.SetActive(set);
                break;
            case ItemType.WATERINGCAN:
                m_WateringActionButton.SetActive(set);
                break;
        }
    }

    private void StartAction()
    {
        m_IsActive = true;
        m_ActionsPanelList.SetActive(false);
        m_CancelActionButton.SetActive(true);
        SetHold?.Invoke(m_IsActive);
    }

    public void DigAction()
    {
        SetAction?.Invoke("dig");
        StartAction();
    }

    public void GetWaterAction()
    {
        SetAction?.Invoke("getwater");
        StartAction();
    }

    public void PlantingAction()
    {
        SetAction?.Invoke("planting");
        StartAction();
    }

    public void WateringAction()
    {
        SetAction?.Invoke("watering");
        StartAction();
    }

    public void PutOutFireAction()
    {
        SetAction?.Invoke("putoutfire");
        StartAction();
    }

    public void CancelAction()
    {
        m_IsActive = false;
        m_ActionsPanelList.SetActive(true);
        m_CancelActionButton.SetActive(false);
        SetHold?.Invoke(m_IsActive);
        StopAction?.Invoke();
        Cursor.visible = true;
    }

    public void CloseActions()
    {
        m_ActionsPanelList.SetActive(false);
        m_OpenActionsButton.SetActive(true);
    }

    public void OpenActions()
    {
        m_OpenActionsButton.SetActive(false);
        m_ActionsPanelList.SetActive(true);
    }



    
}
