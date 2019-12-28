using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedPoint : Point
{
    public Point keyPoint;

    public Color lockedColor;
    public Color oldColor;

    protected override void SetColor()
    {
        if (keyPoint.faction == Faction.PLAYER)
        {
            base.SetColor();
        }
        else
        {
            sprite.color = lockedColor;
        }
    }

    protected override void OnMouseDown()
    {
        if (manager.playerTurn && manager.isVictory == false && keyPoint.faction == Faction.PLAYER)
        {
            CaptureNode();
        }
    }
}
