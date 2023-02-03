using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFireballMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fireball;
    public GameObject[] locationGO;
    float delayfireball;
    void Start()
    {
        delayfireball = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0.1f,0f,0f));
        SpawnFireball();
        delayfireball -= Time.deltaTime;
    }

    void SpawnFireball()
    {
        if (delayfireball <= 0f)
        {
            int a = Random.RandomRange(0, locationGO.Length - 1);
            GameObject fireb = (GameObject)Instantiate(fireball, locationGO[a].transform.position, locationGO[a].transform.rotation);
            Destroy(fireb, 5f);
            delayfireball = 0.1f;
        }
    }
}
