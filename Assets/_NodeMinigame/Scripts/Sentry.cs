using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry : BasePoint
{
    GameObject lineFolder;



    protected override void Start()
    {
        base.Start();
        lineFolder = GameObject.Find("LineFolder");
        CreateConnections();
    }

    protected override void Update()
    {
        //Not sure what to overide this with yet
    }

    public void State()
    {
        var percentageCaptured = ((float) manager.playerNodes / (MatchManager.map.Count - MatchManager.enemies.Count)) * 100;

        Debug.Log(percentageCaptured + "%");

        if(percentageCaptured < 20)
        {
            Idle();
        }
        else if(percentageCaptured >= 20)
        {
            SlowAttack();
        }
        else
        {
            AdvancedAttack();
        }
    }

    private void AdvancedAttack()
    {
        List<Point> targetPoints = new List<Point>();

        foreach(var p in MatchManager.map)
        {
            foreach(var link in p.connections)
            {
                if(link.faction == Faction.ENEMY)
                {
                    targetPoints.Add(link);
                }
            }
        }

        foreach(var target in targetPoints)
        {
            if (target.name == "PlayerBase")
            {
                manager.EnemyCapture(target);
                target.faction = Faction.ENEMY;
            }
            else
            {
                target.faction = Faction.ENEMY;
                break;
            }
        }
    }

    private void SlowAttack()
    {
        StealNodes();
    }

    private void Idle()
    {
        //Idle
    }

    public void StealNodes()
    {
        for (int i = 0; i < connections.Count; i++)
        {
            if (connections[i].faction != Faction.ENEMY && !connections[i].isFortified)
            {
                manager.EnemyCapture(connections[i]);
                connections[i].faction = Faction.ENEMY;
                break;
            }
            else if(connections[i].faction != Faction.ENEMY && connections[i].isFortified)
            {
                break;
            }
        }
    }

    public override bool CheckConnected()
    {
        return true; // does not need to be connected
    }

    public override void CreateConnections()
    {
        // Add one connection to show start point

        GameObject line = GameObject.Instantiate(linePrefab, lineFolder.transform);
        line.GetComponent<LineRenderer>().SetPosition(0, transform.position);
        line.GetComponent<LineRenderer>().SetPosition(1, connections[0].transform.position);
        lines.Add(line);
    }

    public override void UpdateConnections()
    {
        // only update the one connection
        lines[0].GetComponent<LineRenderer>().SetPosition(0, transform.position);
        lines[0].GetComponent<LineRenderer>().SetPosition(1, connections[0].transform.position);
    }
}
