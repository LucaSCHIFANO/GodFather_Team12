using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed = 4.5f;
    public float dgt = 1f;

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BreakableWall")
        {
            collision.gameObject.GetComponent<HPBlock>().removeHP(dgt);
            Destroy(gameObject);
        }
        
        if(collision.gameObject.tag == "Ground") Destroy(gameObject);
    }

}

