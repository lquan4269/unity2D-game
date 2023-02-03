using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorScript : MonoBehaviour
{
    GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            boss.GetComponent<BossMovement>().BossAwake = true;
            boss.GetComponent<BossMovement>().HealthBossPanel.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
