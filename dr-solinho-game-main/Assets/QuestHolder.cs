using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestHolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public static event System.Action<string> ShowHelper;
    public static event System.Action CloseHelper;


    public Text QuestName;
    public Text QuestObjectve;
    public Text QuestObjectveQtd;
    public Image QuestCheck;
    public int Count;
    public string HelperText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowHelper?.Invoke(HelperText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CloseHelper?.Invoke();
    }
}
