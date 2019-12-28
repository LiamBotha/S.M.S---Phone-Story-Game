using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField]
    private int maxHp;
    [HideInInspector]
    public int currentHp;

    private void Awake()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int amount)
    {
        currentHp -= amount;

        if(currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
