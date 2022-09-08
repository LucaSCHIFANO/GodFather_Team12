using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaBall : MonoBehaviour
{
    [SerializeField] private Transform maxHeight;
    private bool goDown;
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    private float currentSpeed;
    [SerializeField] private float maxSpeed;
    
    private void Awake()
    {
        maxHeight = transform.GetChild(0);
        maxHeight.parent = null;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!goDown) currentSpeed += speed;
        else currentSpeed -= speed;
            
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
        rb.velocity = new Vector2(rb.velocity.x, currentSpeed);

        if (transform.position.y >= maxHeight.position.y) goDown = true;
    }
}
