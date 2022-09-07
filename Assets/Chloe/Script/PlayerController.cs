using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Rigidbody2D  rb;
    [SerializeField] private BoxCollider2D  col;

    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    private int playerID = 0;
    [SerializeField] private float dashPower = 24f;
    [SerializeField]private bool onGround = false;
    private int lastDirection = 1; // 1 = droite, -1 = gauche
    private bool isDashing = false;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    private bool canDash = true;

    public GameObject currentParent;
    private Rigidbody2D currentParentRB;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerID);
    }

    private void Update()
    {

        if (isDashing){
            return;
        }
        float moveHorizontal = player.GetAxis("Move Horizontal");
        if (moveHorizontal != 0){
            lastDirection = moveHorizontal > 0 ? 1: -1 ;
        }
        if (currentParent != null)
        {
            moveHorizontal = (moveHorizontal * speed) + currentParentRB.velocity.x;
        } else{
            moveHorizontal = moveHorizontal * speed;
        } 
        rb.velocity = new Vector2(moveHorizontal,rb.velocity.y);

        if (player.GetButtonDown("Jump") && onGround){
            rb.AddForce(Vector2.up * jumpForce);
            onGround = false;
            currentParent = null;
        }

        if(player.GetButtonDown("Dash") && canDash){
            StartCoroutine(Dash());
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Ground")
        {
            if (rb.velocity.y <= 0)
            {
                currentParent = col.gameObject;
                currentParentRB = currentParent.GetComponent<Rigidbody2D>();
            }

            if (!onGround)
            {
                onGround = true;
            }
        }
    }

    private IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower * lastDirection, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;    
    }
}