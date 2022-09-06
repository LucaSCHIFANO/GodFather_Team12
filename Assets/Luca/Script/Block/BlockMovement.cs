using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody2D rb;
        
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void changeSpeed(float speed2)
    {
        speed *= speed2;
        rb.velocity = new Vector2(-speed, 0);
    }
    
    

}
