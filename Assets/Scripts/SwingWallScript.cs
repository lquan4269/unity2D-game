using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingWallScript : MonoBehaviour
{
    public GameObject player;
 
    private void Update()
    {
        CheckSwingWall();
    }

    void CheckSwingWall()
    {
        if (player.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            Collider2D[] wall = Physics2D.OverlapCircleAll(this.transform.position, 0.1f);
            foreach(Collider2D w in wall)
            if (w.gameObject.tag == "Wall")
            {
                player.GetComponent<PlayerController>().SwingWall();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, 0.1f);
    }
}
