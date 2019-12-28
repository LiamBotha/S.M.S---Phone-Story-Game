using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State idleState;
    private State moveState;
    private State attackState;

    public State IdleState
    {
        get
        {
            return idleState;
        }
    }
    public State MoveState
    {
        get
        {
            return moveState;
        }
    }
    public State AttackState
    {
        get
        {
            return attackState;
        }
    }

    State currentState;

    public PlayerController player;
    public GameObject attackParticle;

    public int attackDamage;
    public int speed;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        idleState = new IdleState(this);
        moveState = new MoveState(this);
        attackState = new AttackState(this);

        currentState = IdleState;
    }

    private void Update()
    {
        currentState.Excecute();
    }

    public void ChangeState(State newState)
    {
        currentState = newState;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
