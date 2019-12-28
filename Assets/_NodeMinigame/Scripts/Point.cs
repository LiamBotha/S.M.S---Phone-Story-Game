using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : BasePoint
{
    public int cost = 1;
    public float timeToCapture;
    public bool isFortified;

    GameObject lineFolder;

    protected override void Start()
    {
        base.Start();
        lineFolder = GameObject.Find("LineFolder");
        BuildConnetions();
    }

    protected override void Update()
    {
        SetColor();
        CreateConnections();
        UpdateConnections();
    }

    protected virtual void OnMouseDown()
    {
        if (manager.playerTurn && manager.isVictory == false)
        {
            CaptureNode();
        }
    }

    protected virtual void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Fortify();
        }
    }

    protected virtual void CaptureNode()
    {
        if (faction != Faction.PLAYER && CheckConnected())
        {
            manager.PlayerCapture(cost);
            ChangeFaction();
        }
    }

    protected virtual void Fortify()
    {
        if (faction == Faction.PLAYER && isFortified == false && CheckConnected())
        {
            manager.remainingActions--;
            isFortified = true;
            Debug.Log("The point has been fortifyed");
        }
    }

    protected virtual void SetColor()
    {
        switch (faction)
        {
            case Faction.PLAYER:
                sprite.color = playerColor;
                break;
            case Faction.NEUTRAL:
                sprite.color = neutralColor;
                break;
            case Faction.ENEMY:
                sprite.color = enemyColor;
                break;
        }
    }

    protected virtual void ChangeFaction()
    {
        this.faction = Faction.PLAYER;
    }

    public override bool CheckConnected()
    {
        foreach (BasePoint connection in MatchManager.map)
        {
            foreach (BasePoint link in connection.connections)
            {
                if (link == this && connection.faction == Faction.PLAYER)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void BuildConnetions()
    {
        foreach (var link in connections)
        {
            GameObject line = GameObject.Instantiate(linePrefab, lineFolder.transform);
            line.GetComponent<LineRenderer>().SetPosition(0, transform.position);
            line.GetComponent<LineRenderer>().SetPosition(1, link.transform.position);
            lines.Add(line);
        }
    }

    public override void CreateConnections()
    {
        for (int i = 0; i < connections.Count; i++)
        {
            while (lines.Count < connections.Count)
            {
                GameObject line = GameObject.Instantiate(linePrefab,lineFolder.transform);
                line.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                line.GetComponent<LineRenderer>().SetPosition(1, connections[connections.Count - lines.Count].transform.position);
                lines.Add(line);
            }
        }
    }

    public override void UpdateConnections()
    {
        for (int i = 0; i < connections.Count; i++)
        {
            lines[i].GetComponent<LineRenderer>().SetPosition(0, transform.position);
            lines[i].GetComponent<LineRenderer>().SetPosition(1, connections[i].transform.position);
        }
    }
}
