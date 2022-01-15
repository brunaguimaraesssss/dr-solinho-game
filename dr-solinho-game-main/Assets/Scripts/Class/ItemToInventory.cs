using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemToInventory :MonoBehaviour
{
        [SerializeField]
        private GameObject m_Panel = null;
        [SerializeField]
        private Button m_YesButton = null;

    public void Start()
    {
        MouseSensitive.SetPanel += this.Set;
    }

    private void OnDestroy() =>
        MouseSensitive.SetPanel -= this.Set;

    public abstract void Set(MouseSensitive mouse);
   
    public void ResetButton()
    {
        this.m_Panel.SetActive(false);
        this.m_YesButton.onClick.RemoveAllListeners();
    }

    public void TurnOn() =>
        this.m_Panel.SetActive(true);


    public void SetYesButtonListener(UnityEngine.Events.UnityAction command) =>
        this.m_YesButton.onClick.AddListener(command);


}
