using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class Gizmoes : MonoBehaviour
{
    List<Point> points = new List<Point>();

    private void Awake()
    {
        points = GameObject.FindObjectsOfType<Point>().ToList();
    }

    private void OnDrawGizmos()
    {
        foreach (var p in points)
        {
            for (int i = 0; i < p.connections.Count; i++)
            {
                Gizmos.DrawLine(p.transform.position, p.connections[i].transform.position);
            }
        }
    }
}
