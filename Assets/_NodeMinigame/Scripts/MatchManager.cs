using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchManager : MonoBehaviour
{

    public bool playerTurn = true;
    public bool isVictory = false;

    public int remainingActions;
    public readonly int maxActions = 2;
    public int playerNodes = 0;

    public static List<BasePoint> map = new List<BasePoint>();
    public static List<Terminal> terminals = new List<Terminal>();
    public static List<Sentry> enemies = new List<Sentry>();

    public Point playerBase;

    public static event Action<MatchManager> onVictory = delegate { };

    private void Start()
    {
        remainingActions = maxActions;
        map = FindObjectsOfType<BasePoint>().ToList();
        terminals = FindObjectsOfType<Terminal>().ToList();
        enemies = FindObjectsOfType<Sentry>().ToList();

        foreach(var value in map)
        {
            value.manager = this;
        }
    }

    private void Update()
    {
        if (!VictoryCheck())
        {
            if (remainingActions != 0)
            {
                PlayerActions();
            }
            else
            {
                EnemyActions();
            }
        }
    }

    public void PlayerCapture(int cost)
    {
        remainingActions -= cost;
        playerNodes++;
    }

    public void EnemyCapture(Point toCapture)
    {
        if (toCapture.faction == BasePoint.Faction.PLAYER)
        {
            playerNodes--;
        }
    }

    private bool VictoryCheck()
    {
        if (playerBase.faction != BasePoint.Faction.PLAYER)
        {
            Debug.Log("The Player has been defeated");
        }

        isVictory = true;
        foreach (var t in terminals)
        {
            if(t.faction == BasePoint.Faction.PLAYER)
            {
                continue;
            }
            isVictory = false;
        }

        if(isVictory)
        {
            Debug.Log("All Terminals have been captured");
            onVictory(this);
            return true;
        }
        return false;
    }

    private void PlayerActions()
    {
        playerTurn = true;
    }

    private void EnemyActions()
    {
        playerTurn = false;

        foreach(var enemy in enemies)
        {
            enemy.State();
        }

        remainingActions = maxActions;

    }

    IEnumerator RestoreActions()
    {
        yield return new WaitForSeconds(1f);
        remainingActions = maxActions;
    }
}
