using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBody : MonoBehaviour
{
    [SerializeField] private float id;
    private Animator anim;
    private void Awake()
    {
        //anim.enabled = false;
        anim = GetComponent<Animator>();
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(0.2f * id);
        anim.enabled = true;
    }
    
    /*[SerializeField] private Transform parent;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    private float actualSpeed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if(transform)
        
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, actualSpeed);
        transform.position = new Vector2(parent.position.x, transform.position.y);
    }*/
}
