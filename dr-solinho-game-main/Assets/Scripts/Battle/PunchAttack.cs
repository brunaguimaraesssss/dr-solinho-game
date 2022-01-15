using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : Attacks
{
    public void Punch()
    {
        CallAttack();
        ActiveteEffect();
    }
}
