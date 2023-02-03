using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator aim;

    private float move;
    private float movementSpeed;
    private float jumpForce;


    private bool isFacingRight;
    private bool Walking;
    public bool onGround;
    public bool attack; //chung
    private float comboAttack;
    private bool isUsingSkill; 
    public GameObject skillOneLocation;
    public GameObject skillOneEffect;
    public GameObject Effect;
    GameObject HeartContent;

    float maxHearth;
    float currentHeart;
    public Image imageHearth;
    GameObject scoreText;
    int scoreInt;
    bool freezepositionbool;
    GameObject maincamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aim = GetComponent<Animator>();
        Walking = false;
        isFacingRight = true;
        movementSpeed = 5.0f;
        jumpForce = 5.0f;
        onGround = true;//check dieu kien dung tren mat dat
        comboAttack = 1;
        isUsingSkill = false;
        HeartContent = GameObject.Find("HeartContent");
        maxHearth = 3f;
        currentHeart = 3f;
        scoreInt = 0;
        scoreText = GameObject.Find("ScoreText");
        freezepositionbool = false;
        maincamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update() //ten ham unity //bat buoc
    {
        Checkinput();
        ApplyMovement();
        CheckMovementDirection();
        CheckOnGround();
        UpdateAnimation();

    }
 
    private void CheckMovementDirection()
    {
        if (isFacingRight && move < 0)  //xoay trai xoay phai
        {
            Flip();
        }
        else if (!isFacingRight && move >0) 
        {
            Flip();
        }

        if (move != 0)
        {
            Walking = true;
        }
        else
        {
            Walking = false;
        }
    }
    private void UpdateAnimation() 
    {
        aim.SetBool("Walking",Walking);
        aim.SetBool("Attack", attack);
        aim.SetFloat("yVelocity", rb.velocity.y);
        aim.SetBool("Grounded", onGround);
        aim.SetFloat("ComboA",comboAttack);
        aim.SetBool("Skill",isUsingSkill);
        aim.SetBool("IsSwingWal",freezepositionbool);
    }
    private void Checkinput() 
    {
        move = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && rb.velocity.y == 0) //nhan nut
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Z) && attack == false && isUsingSkill == false) //chua danh // k su dung don danh trong luc skill
        {
            //switch case
            comboAttack += 1;
            attack = true;
            if (comboAttack > 2)
            {
                comboAttack = 1;
            }
            //if (comboAttack == 1)
            //{
            //    aim.Play("attack1");
            //    attack = true;
            //}
            //else if (comboAttack == 2)
            //{
            //    aim.Play("attack2");
            //    attack = true;
            //    comboAttack = 0;
            //}
        }
        if (Input.GetKeyDown(KeyCode.X) && attack == false && isUsingSkill == false)
        {
            isUsingSkill = true;
            aim.Play("skill"); 
            GameObject skillone = (GameObject)Instantiate(skillOneEffect, skillOneLocation.transform.position, skillOneLocation.transform.rotation);
            if (isFacingRight == true)
            {
                skillone.GetComponent<SkillOneMovement>().speed *= 1;
            }
            else
            {
                skillone.GetComponent<SkillOneMovement>().speed *= -1;
            }
            Destroy(skillone, 3f);
        }
    }
    private void Jump()//nhay
    {
        if (freezepositionbool == true) 
        {
            rb.simulated = true;
            freezepositionbool = false;
        }
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void ApplyMovement() //vector3 3 chieu
    {
        if (freezepositionbool != true)
        {
            transform.position = transform.position + new Vector3(move * movementSpeed * Time.deltaTime, 0f, 0f);
        }
    }

    private void Flip() 
    {
        if (freezepositionbool != true)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void CheckOnGround() 
    {
        if (rb.velocity.y != 0)
        {
            onGround = false;   
        }
        else
        {
            onGround = true;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    public void EndSkillOne()
    {
        isUsingSkill = false;
    }

    public void EndAttack()
    {
        attack = false;
    }

    public void AttackEnemy()
    {
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(skillOneLocation.transform.position,0.5f);
        foreach (var enemy in Enemies)
        {
            if (enemy.tag == "enemy")
            {
                GameObject effect = (GameObject)Instantiate(Effect, enemy.transform.position, enemy.transform.rotation);
                Destroy(effect,0.2f);
                enemy.GetComponent<scrpitEnemy>().DenineHealth();
            }
            if (enemy.tag == "boss")
            {
                GameObject effect = (GameObject)Instantiate(Effect, enemy.transform.position, enemy.transform.rotation);
                Destroy(effect, 0.2f);
                enemy.GetComponent<BossMovement>().DenineHealth();
            }

        }
    }

    public void HitDamage() 
    {
        currentHeart--;
        Transform child = HeartContent.transform.GetChild(HeartContent.transform.childCount - 1);
        Destroy(child.gameObject);
        if (currentHeart <= 0)
        {
            maincamera.GetComponent<AllBtnScript>().ShowOverPanel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hearth") 
        {
            if (currentHeart < maxHearth)
            {
                currentHeart++; //tang mau
                Image hearth = (Image)Instantiate(imageHearth,HeartContent.transform.position, HeartContent.transform.rotation); //hoi mau
                hearth.transform.SetParent(HeartContent.transform);
                Destroy(collision.gameObject);
            }
        }
    }

    public void PlussScore()
    {
        scoreInt++;
        scoreText.GetComponent<Text>().text = "Score: " + scoreInt.ToString();
    }

    public void SwingWall()
    {
        rb.velocity = new Vector2(0f, 0f);
        rb.simulated = false;
        freezepositionbool = true;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(skillOneLocation.transform.position,0.5f);
    }
}
