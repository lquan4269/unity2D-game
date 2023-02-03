using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOneMovement : MonoBehaviour
{
    public float speed = 3f;
    void Update()
    {
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime , transform.position.y);
    }
}
