using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private float cooldown = 0;
    private float maxCooldown = 0.2f;

    public AttackState(StateMachine controller) : base(controller)
    {

    }

    public override void Excecute()
    {
        LookAtPlayer(controller.player);

        if (cooldown <= 0)
        {
            cooldown = maxCooldown;
            controller.player.TakeDamage(controller.attackDamage);
            GameObject.Instantiate(controller.attackParticle,controller.transform);
        }
        else
        {
            cooldown -= Time.deltaTime;
        }

        var distance = Vector3.Distance(controller.transform.position, controller.player.transform.position);

        if (distance > 3)
        {
            controller.ChangeState(controller.MoveState);
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
