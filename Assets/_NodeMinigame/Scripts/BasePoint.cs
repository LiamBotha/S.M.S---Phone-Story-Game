using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePoint : MonoBehaviour
{
    public enum Faction
    {
        PLAYER, NEUTRAL, ENEMY
    }

    public Faction faction;

    protected SpriteRenderer sprite;
    [SerializeField] protected Color playerColor;
    [SerializeField] protected Color enemyColor;
    [SerializeField] protected Color neutralColor;

    protected List<GameObject> lines = new List<GameObject>();
    public List<Point> connections = new List<Point>();

    public GameObject linePrefab;
    [HideInInspector] public MatchManager manager;

    protected virtual void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    protected abstract void Update();

    public abstract bool CheckConnected();

    public abstract void CreateConnections();

    public abstract void UpdateConnections();

}
