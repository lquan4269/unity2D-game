using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemyScript : MonoBehaviour
{
    bool enemyalive;
    bool enemyDestroyed;
    float timeSpawnEnemy;
    public GameObject enemy;
    private void Start()
    {
        enemyalive = true;
        enemyDestroyed = false;
        timeSpawnEnemy = 0f;
    }
    void Update()
    {

        if (enemyDestroyed == false)
        {
            Checkenemy();
        }
        SpawnEnemy();
    }

    bool Checkenemy() 
    {
        enemyalive = false;
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(this.transform.position, 5f);//vung check va cham
        foreach (var enemy in Enemies) //check vung tao
        {
            if (enemy.tag == "enemy")
            {
                enemyalive = true;
                return enemyalive;
            }
        }
        if (enemyalive == false)
        {
            timeSpawnEnemy = 3f;
            enemyDestroyed = true;
        }
        return enemyalive;
    }

    void SpawnEnemy()
    {
        timeSpawnEnemy -= Time.deltaTime;
        if (timeSpawnEnemy <= 0 && Checkenemy() == false)
        {
            Instantiate(enemy,this.transform.position,this.transform.rotation);
            enemyalive = true;
            enemyDestroyed = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 5f);
    }
}
