using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{

    public MoveState(StateMachine controller) : base(controller)
    {

    }

    public override void Excecute()
    {
        LookAtPlayer(controller.player);

        controller.transform.position = Vector3.MoveTowards(controller.transform.position, controller.player.transform.position, controller.speed * Time.deltaTime);
        Debug.Log("MoveState");

        var distance = Vector3.Distance(controller.transform.position, controller.player.transform.position);

        if (distance <= 3)
        {
            controller.ChangeState(controller.AttackState);
        }
    }

    void LookAtPlayer(PlayerController player)
    {
        Vector3 targetPosition = player.transform.position;
        Vector3 dir = targetPosition - controller.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        controller.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
}
