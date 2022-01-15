using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationManager : MonoBehaviour
{
    public Text NameText;
    public Text DescriptionText;
    public Text QuantityText;
    public Text PriceText;
    public GameObject InformationArea;


    public void Start()
    {
        MouseSensitive.MouseOn += ShowInformations;
        MouseSensitive.MouseOFF += ResetInformations;
    }

    private void OnDestroy()
    {        
        MouseSensitive.MouseOn -= ShowInformations;
        MouseSensitive.MouseOFF -= ResetInformations;        
    }

    public void HideInformation() => 
        InformationArea.SetActive(false);

    private void ShowInformations(string name, string description, string qtd, string price)
    {
        InformationArea.SetActive(true);
        NameText.text = name.ToUpper();
        DescriptionText.text = description.ToUpper();
        QuantityText.text = qtd;
        PriceText.text = price;
    }

    private void ResetInformations()
    {
        InformationArea.SetActive(false);
        NameText.text = string.Empty;
        DescriptionText.text = string.Empty;
        QuantityText.text = string.Empty;
    }

}
