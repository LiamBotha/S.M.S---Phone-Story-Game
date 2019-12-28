using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryPoint : Point
{
    public int failPercentage = 50;
    bool failed = false;

    protected override void CaptureNode()
    {
        if (faction != Faction.PLAYER && CheckConnected())
        {
            manager.PlayerCapture(cost);
            int r = UnityEngine.Random.Range(0, 101);
            if (r <= failPercentage)
            {
                StunPlayer();
            }
            else ChangeFaction();
        }
    }

    private void StunPlayer()
    {
        faction = Faction.ENEMY;

    }
}
