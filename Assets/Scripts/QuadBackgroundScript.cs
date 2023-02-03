using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadBackgroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    Renderer bgRend;
    float speed = 0.1f;
    void Start()
    {
        bgRend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        if (inputH > 0)
        {
            bgRend.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0f);
        }
        if (inputH < 0)
        {
            bgRend.material.mainTextureOffset -= new Vector2(speed * Time.deltaTime, 0f);
        }
    }
}
