using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int maxHp;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;

    [SerializeField] protected int maxRange;
    [SerializeField] protected int attackRange;

    protected int currentHp;
    protected bool usedSpecial;
    private float cooldown = 0;
    private float maxCooldown = 0.2f;

    public static event Action<Enemy> OnAnyReachGoal = delegate { };

    private States currState;

    private enum States
    {
        IDLE,MOVE,ATTACK,SPECIAL
    }

    internal void TakeDamage(int amount)
    {
        currentHp -= amount;
        if(currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected GameObject player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        currentHp = maxHp;
    }

    private void Update()
    {
        ChangeState();
        Act();
    }

    protected virtual void Act()
    {
        switch (currState)
        {
            case States.IDLE: Idle();
                break;
            case States.MOVE: Move();
                break;
            case States.ATTACK: Attack();
                break;
            case States.SPECIAL: Special();
                break;
        }
    }

    void ChangeState()
    {
        var hpPercentage = (currentHp * 100) / maxHp;
        var distance = Vector3.Distance(transform.position, player.transform.position);

        if (hpPercentage <= 25)
        {
            currState = States.SPECIAL;
        }
        else if (distance <= attackRange)
        {
            currState = States.ATTACK;
        }
        else if (distance <= maxRange)
        {
            currState = States.MOVE;
        }
        else
        {
            currState = States.IDLE;
        }
    }

    protected virtual void Idle()
    {
        Vector3 movement = Vector3.down;
        transform.position += movement * speed * Time.deltaTime;
        Debug.Log("Idle");
    }

    protected virtual void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Debug.Log("Move");
    }

    protected virtual void Attack()
    {
        if (cooldown <= 0)
        {
            cooldown = maxCooldown;
            player.GetComponent<PlayerController>().TakeDamage(damage);
        }
        else
        {
            cooldown -= Time.deltaTime;
        }


    }

    protected virtual void Special()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyGoal")
        {
            OnAnyReachGoal(this);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
