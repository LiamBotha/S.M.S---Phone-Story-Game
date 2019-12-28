using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;

    [SerializeField] private int maxHp;
    [SerializeField] private float speed;
    [SerializeField] private int attack;

    [HideInInspector]
    public int currentHp;
    private float cooldown;

    [SerializeField] private GameObject projectile;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHp = maxHp;
	}
	
	// Update is called once per frame
	void Update ()
    {
        LookAtMouse();

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, vertical,0);
        transform.position += (move * speed * Time.deltaTime);
        
        if(Input.GetMouseButton(0))
        {
            if (cooldown <= 0)
            {
                cooldown = 0.2f;
                Fire();
            }
        }

        cooldown -= Time.deltaTime;
    }

    public void TakeDamage(int amount)
    {
        currentHp -= amount;
        if(currentHp <= 0)
        {
            Debug.Log("Player Died");
        }
    }

    private void Fire()
    {
        GameObject fire = GameObject.Instantiate(projectile);
        fire.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        fire.transform.rotation = transform.rotation;
        fire.GetComponent<Projectile>().damage = attack;
    }

    void LookAtMouse()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
    }

    internal void Reset()
    {
        currentHp = maxHp;
    }
}
