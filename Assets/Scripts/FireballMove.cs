using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0.5f, 0f, 0f));
        Checkoverlap();
    }

    void Checkoverlap()
    {
        Collider2D[] gos = Physics2D.OverlapCircleAll(new Vector2(this.transform.position.x+0.3f, this.transform.position.y), 0.5f);
        foreach (var go in gos)
        {
            if (go.gameObject.tag == "Wall")
            {
                Destroy(this.gameObject);
            }
            if (go.gameObject.tag == "Player")
            {
                go.GetComponent<PlayerController>().HitDamage();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(this.transform.position.x + 0.3f, this.transform.position.y), 0.5f);
    }
}
