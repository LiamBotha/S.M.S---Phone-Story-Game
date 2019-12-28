using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(StateMachine controller) : base(controller)
    {

    }

    public override void Excecute()
    {
        Vector3 movement = Vector3.down;

        controller.transform.position += movement * controller.speed * Time.deltaTime;

        Debug.Log("IdleState");

        var distance = Vector3.Distance(controller.transform.position, controller.player.transform.position);

        if(distance <= 8)
        {
            controller.ChangeState(controller.MoveState);
        }
    }
}
