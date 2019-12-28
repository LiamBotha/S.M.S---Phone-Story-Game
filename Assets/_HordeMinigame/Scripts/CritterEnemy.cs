using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterEnemy : Enemy
{
    protected override void Special()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -1 * speed * Time.deltaTime);
    }
}
