using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] monsterToSpawn;

    private float spawnTime = 1;
    private float cooldown = 0;

    private void Update()
    {
        if(cooldown <= 0)
        {
            cooldown = spawnTime;
            SpawnMonster();
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    public void SpawnMonster()
    {
        int randXPos = Random.Range(-5, 6);
        int randYPos = Random.Range(6, 9);
        int randMonster = Random.Range(0, monsterToSpawn.Length);

        GameObject spawnedMonster = Instantiate(monsterToSpawn[randMonster], new Vector3(randXPos, randYPos, 0), Quaternion.identity);
    }
}
