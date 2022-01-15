using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private GameObject m_DescriptionWindow = null;
    [SerializeField]
    private Vector3 m_InitialPos = Vector3.zero;
    [SerializeField]
    private Text m_DescriptionText = null;
    [SerializeField]
    private string m_ButtonName = null;

    private bool m_IsShowDescription;

    public void OnPointerClick(PointerEventData eventData)
    {

        m_DescriptionWindow.SetActive(true);
        m_DescriptionWindow.GetComponent<RectTransform>().anchoredPosition = m_InitialPos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!m_IsShowDescription)
        {
            m_IsShowDescription = true;
            m_DescriptionWindow.SetActive(true);
            m_DescriptionWindow.transform.position = MousePosition();
            m_DescriptionText.text = m_ButtonName.ToUpper();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_IsShowDescription = false;
        m_DescriptionWindow.SetActive(false);
    }

    private Vector3 MousePosition()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(pos.x, pos.y + 0.5f, 0);
    }
}
