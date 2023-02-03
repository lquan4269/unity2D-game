using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] pos;
    int i;
    float delay;
    float delaySkill;
    float a;

    private GameObject player;
    public GameObject spawnFireballGO;
    public GameObject spawnFireballLocation;

    public GameObject spawnFirespawnSkillTwo;
    public GameObject spawnFirespawnSkillTwob;
    public GameObject fireballGO;
    public GameObject HealthBoss;
    public GameObject HealthBossPanel;
    float health;
    float maxhealth;

    bool skilltwo;
    public bool BossAwake;
    float delayskillTwo;

    Animator anim;

    void Start()
    {
        this.transform.position = pos[0].position;
        i = 0;
        delay = 5f;
        delaySkill = 3f;
        a = 0;
        skilltwo = false;
        anim = GetComponent<Animator>();
        delayskillTwo = 0.05f;
        health = 10;
        maxhealth = 10;
        player = GameObject.Find("Player");
        HealthBoss = GameObject.Find("HealthBoss");
        HealthBossPanel = GameObject.Find("HealthBossPanel");
        HealthBossPanel.SetActive(false);
        BossAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (BossAwake == true) 
        {
            MoveWithBoss();
            UseSkill();
            a -= Time.deltaTime;

            if (skilltwo == true)
            {
                delayskillTwo -= Time.deltaTime;
                if (delayskillTwo <= 0f)
                {
                    UseSkillTwo();
                    delayskillTwo = 0.1f;
                }
            }
            anim.SetBool("UsingSkillTwo", skilltwo);
        }
    }

    void MoveWithBoss()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            while (true)
            { 
                int a = Random.Range(0, 3);
                if (a != i)
                {
                    i = a;
                    delay = 10f;
                    break;
                }
            }
        }
        transform.position = Vector3.MoveTowards(transform.position,pos[i].position, 10f*Time.deltaTime);
        if (Vector2.Distance(this.transform.position, pos[i].position) <= 2f)
        {
            delaySkill -= Time.deltaTime;
        }
        else
        {
            delaySkill = 3f;
        }

        if (this.transform.position.x <= 65f)
        {
            this.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0f,180f,0f);
        }
    }

    void UseSkill() 
    {
        if (delaySkill <= 0)
        {
            if (this.transform.position.y <= 0 && this.transform.position.y >= -1)
            {
                skilltwo = true;
                delaySkill = 3f;
            }
            else
            {
                UseSkillOne();
                delaySkill = 3f;
            }
        }
    }

    void UseSkillOne()
    {
        GameObject sfireball = (GameObject)Instantiate(spawnFireballGO, spawnFireballLocation.transform.position, spawnFireballLocation.transform.rotation);
        Destroy(sfireball, 10f);
    }

    void UseSkillTwo()
    {
        GameObject fireball = (GameObject)Instantiate(fireballGO, spawnFirespawnSkillTwo.transform.position, spawnFirespawnSkillTwo.transform.rotation);
        GameObject fireballb = (GameObject)Instantiate(fireballGO, spawnFirespawnSkillTwob.transform.position, spawnFirespawnSkillTwob.transform.rotation);
        Destroy(fireball, 10f);
        Destroy(fireballb, 10f);
    }

    public void EndSkillTwo()
    {
        skilltwo = false;
    }

    public void DenineHealth()
    {
        health -= 1;
        if (health <= 0)
        {
            player.GetComponent<PlayerController>().PlussScore();
            HealthBossPanel.SetActive(false);
            Destroy(this.gameObject);
        }

        HealthBoss.GetComponent<UnityEngine.UI.Image>().fillAmount = health/maxhealth;
    }
}
