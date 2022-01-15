using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : RemoveItem
{
    
    override public void Set(MouseSensitive mouse)
    {
        if(mouse.GetVendorItem())
        {
            TurnOn();
            SetYesButtonListener(mouse.BuyItem);
        }
    }
}
