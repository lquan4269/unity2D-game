using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrpitEnemy : MonoBehaviour
{
    public bool AttE;
    private Animator aim;
    private GameObject player;
    private SpriteRenderer spriteRen;
    private float speed = 5f;
    private float fore = 1f;
    private bool doOnce = false;
    private float repeatMovement;
    int health;
    // Start is called before the first frame update
    void Start()
    {
        aim = GetComponent<Animator>();
        spriteRen = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        repeatMovement = 0f;
        health = 3;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(this.transform.position, player.transform.position) <= 2.5f && player.GetComponent<PlayerController>().onGround == true)
        {

            AttE = true;
            CheckPositionPlayer();
            speed = 0f;
        }
        else
        {
            AttE = false;
            Movement();
        }
        aim.SetBool("AttE", AttE);
    }

    void Movement() 
    {
        if (spriteRen.flipX == true)
        {
            speed = -5f;
        }
        else
        {
            speed = 5f;
        }
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }

    void CheckPositionPlayer()
    {
        if (player.transform.position.x < this.transform.position.x)
        {
            spriteRen.flipX = true;
        }
        else
        {
            spriteRen.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //vua cham
    {
        if (collision.gameObject.tag == "WallRight")
        {
            spriteRen.flipX = true;
        }
        if (collision.gameObject.tag == "WallLeft")
        {
            spriteRen.flipX = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) //ben trong
    {
        if (collision.gameObject.tag == "WallRight")
        {
            spriteRen.flipX = true;
        }
        if (collision.gameObject.tag == "WallLeft")
        {
            spriteRen.flipX = false;
        }
    }

    public void DenineHealth() 
    {
        health -= 2;
        if (health <=0) 
        {
            player.GetComponent<PlayerController>().PlussScore();
            Destroy(this.gameObject);
        }
    }

    public void AttackPlayer()
    {
        player.GetComponent<PlayerController>().HitDamage();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position,2f);
    }
}
