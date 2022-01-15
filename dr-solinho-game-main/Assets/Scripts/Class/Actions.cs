using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actions : MonoBehaviour, IGameStart
{

    private AudioClip m_SucessSFX;
    private AudioClip m_ErrorSFX;
    private AudioSource m_AudioSource;
    private bool m_IsActive;

    [SerializeField]
    private string ActionName = string.Empty;

    [SerializeField]
    private bool m_IsQuest = false;

    [SerializeField]
    private int m_QuestID =0;



    public static event System.Action<int> Quest;
    public static event System.Action<ItemScript> ToInventory;
    public static event System.Action<ItemReference> UpdateQtd;
    public static event System.Action<ItemScript> Remove;

    public void Init()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_SucessSFX = Resources.Load("sucessSFX") as AudioClip;
        m_ErrorSFX = Resources.Load("errorSFX") as AudioClip;
        ActionsManager.SetAction += StartAction;
        ActionsManager.StopAction += StopAction;
    }

    private void OnDestroy()
    {
        ActionsManager.SetAction -= StartAction;
        ActionsManager.StopAction -= StopAction;
    }

    private void LateUpdate()
    {
        if(m_IsActive)
            Control();
    }

    private void StartAction(string action) =>
           m_IsActive = action.Equals(ActionName);

    private void StopAction() =>
        m_IsActive = false;

    public void QuestCount()
    {
        if(m_IsQuest)
        {
            Quest?.Invoke(m_QuestID);

        }

    }

    public void Sucess() => 
        m_AudioSource.PlayOneShot(m_SucessSFX);
    
    public void Failed() => 
        m_AudioSource.PlayOneShot(m_ErrorSFX);

    public void ToInventoryEvent(ItemScript item) =>
        ToInventory?.Invoke(item);
    

    public void UpdateQtdEvent(ItemReference item) =>
        UpdateQtd?.Invoke(item);
    

    public void RemoveEvent(ItemScript item) =>
        Remove?.Invoke(item);
    

    public bool HaveItem(string itemName)
    {
        foreach(var obj in CreateMenu.InventoryList)
        {
            if(obj.GetItem().Name.Contains(itemName))
            {
                RemoveEvent(obj.GetItem());
                UpdateQtdEvent(obj);
                return true;
            }
        }

        return false;
    }


    abstract public void Control();




}
