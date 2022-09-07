using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{

    [SerializeField] private float speedH;
    [SerializeField] private float speedV;
    private Rigidbody2D rb;
        
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void changeSpeed(Vector2 speed2)
    {
        speedH *= speed2.x;
        speedV *= speed2.y;
        rb.velocity = new Vector2(-speedH, -speedV);
    }
    
    

}
