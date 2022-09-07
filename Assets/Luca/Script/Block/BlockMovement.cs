using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{

    [SerializeField] private float speedH;
    [SerializeField] private float speedV;
    private Rigidbody2D rb;

    [SerializeField] private float timeBeforeDelete;
        
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeBeforeDelete);
    }

    public void changeSpeed(Vector2 speed2)
    {
        speedH *= speed2.x;
        speedV *= speed2.y;
        rb.velocity = new Vector2(-speedH, -speedV);
    }
    
    

}
