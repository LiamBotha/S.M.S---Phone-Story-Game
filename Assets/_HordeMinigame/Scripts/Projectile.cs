using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;

	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.up * 15 * Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy target = collision.GetComponent<Enemy>();
        if(target != null)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }
        EnemyHp enemyHp = collision.GetComponent<EnemyHp>();
        if(enemyHp != null)
        {
            enemyHp.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
