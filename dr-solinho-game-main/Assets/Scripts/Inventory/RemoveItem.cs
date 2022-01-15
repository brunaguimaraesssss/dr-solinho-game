using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveItem : ItemToInventory
{
    
    override public void Set(MouseSensitive mouse)
    {
        if(!mouse.GetVendorItem())
        {
            TurnOn();
            SetYesButtonListener(mouse.RemoveItem);
        }
    }
}
