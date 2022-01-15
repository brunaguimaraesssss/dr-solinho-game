using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_VendorPanel = null;
    [SerializeField]
    private ItemReference m_Element = null;
    [SerializeField]
    private GameObject m_Parent = null;
    [SerializeField]
    private Text m_TotalCoin = null;

    [SerializeField]
    private List<ItemScript> m_VendorList = new List<ItemScript>();


    private AudioClip m_SucessSFX;
    private AudioClip m_ErrorSFX;
    private AudioSource m_AudioSource;

    void Start()
    {
        Fungus.FungusPrioritySignals.OnFungusPriorityEnd += CloseVendor;
        MouseSensitive.UpdateCoin += UpdateTotalCoin;
        MouseSensitive.UpdateCoin += PlaySound;
        m_AudioSource = GetComponent<AudioSource>();
        m_SucessSFX = Resources.Load("sucessSFX") as AudioClip;
        m_ErrorSFX = Resources.Load("errorSFX") as AudioClip;

        PopulateVendor();

    }
    private void OnDestroy()
    {
        Fungus.FungusPrioritySignals.OnFungusPriorityEnd -= CloseVendor;
        MouseSensitive.UpdateCoin -= UpdateTotalCoin;
        MouseSensitive.UpdateCoin -= PlaySound;
    }


    private void UpdateTotalCoin(bool sucess)
    {
        if(Inventory.GetCoin() != null)
            m_TotalCoin.text = Inventory.GetCoin().Count.ToString("00");
        else
            m_TotalCoin.text = "00";
    }


    public void OpenVendor()
    {
        m_VendorPanel.SetActive(true);
        UpdateTotalCoin(true);
    }

    public void CloseVendor()=>
        m_VendorPanel.SetActive(false);


    private void PopulateVendor()
    {
        foreach(ItemScript item in m_VendorList)
        {
            var obj = Instantiate(m_Element, m_Parent.transform);
            obj.GetComponent<ItemReference>().SetValues(item);
            obj.GetComponent<MouseSensitive>().SetVendorItem(true);
        }
    }

    private void PlaySound(bool sucess)
    {
        if(sucess)
            m_AudioSource.PlayOneShot(m_SucessSFX);
        else
            m_AudioSource.PlayOneShot(m_ErrorSFX);

    }
}
