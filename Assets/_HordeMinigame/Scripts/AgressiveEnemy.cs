using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveEnemy : Enemy
{
    protected override void Special()
    {
        if(!usedSpecial)
        {
            currentHp = 5;
            speed = 9;
            usedSpecial = true;
        }
        else
        {
            Move();
        }
    }
}
