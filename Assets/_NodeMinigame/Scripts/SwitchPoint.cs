using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPoint : BasePoint
{

    public bool isActive = true;

    public List<Point> setA = new List<Point>();
    public List<Point> setB = new List<Point>();

    public Point previousPoint;

    GameObject lineFolder;

    private void OnMouseDown()
    {
        isActive = !isActive;
        manager.remainingActions--;
    }

    protected override void Start()
    {
        base.Start();
        lineFolder = GameObject.Find("LineFolder");
        connections = setA;
        CreateConnections();
    }

    protected override void Update()
    {
        SwitchState();
        UpdateConnections();
        if(previousPoint.faction == Faction.PLAYER)
        {
            faction = Faction.PLAYER;
        }
    }

    private void SwitchState()
    {

        if(isActive)
        {
            connections = setA;
        }
        else
        {
            connections = setB;
        }
    }

    public override bool CheckConnected()
    {
        throw new System.NotImplementedException();
    }

    public override void CreateConnections()
    {
        GameObject line = GameObject.Instantiate(linePrefab, lineFolder.transform);
        line.GetComponent<LineRenderer>().SetPosition(0, transform.position);
        line.GetComponent<LineRenderer>().SetPosition(1, connections[0].transform.position);
        lines.Add(line);

        GameObject linePrevious = GameObject.Instantiate(linePrefab, lineFolder.transform);
        line.GetComponent<LineRenderer>().SetPosition(0, transform.position);
        line.GetComponent<LineRenderer>().SetPosition(1, previousPoint.transform.position);
        lines.Add(linePrevious);
    }

    public override void UpdateConnections()
    {
        lines[0].GetComponent<LineRenderer>().SetPosition(0, transform.position);
        lines[0].GetComponent<LineRenderer>().SetPosition(1, connections[0].transform.position);

        lines[1].GetComponent<LineRenderer>().SetPosition(0, transform.position);
        lines[1].GetComponent<LineRenderer>().SetPosition(1, previousPoint.transform.position);
    }
}
