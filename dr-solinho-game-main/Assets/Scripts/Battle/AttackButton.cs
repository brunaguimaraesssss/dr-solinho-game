using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private GameObject m_DescriptionWindow = null;
    [SerializeField]
    private Text m_DescriptionText = null;
    [SerializeField]
    private string m_ButtonName = null;

    private bool m_IsShowDescription;

    public void OnPointerClick(PointerEventData eventData)
    {

        m_DescriptionWindow.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!m_IsShowDescription)
        {
            m_IsShowDescription = true;
            m_DescriptionWindow.SetActive(true);
            m_DescriptionText.text = m_ButtonName.ToUpper();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_IsShowDescription = false;
        m_DescriptionWindow.SetActive(false);
    }
}
