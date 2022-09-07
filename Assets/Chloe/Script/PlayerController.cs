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
    [SerializeField] private int dashSpeed;
    [SerializeField]private bool isJumping = false;
    private int lastDirection = 1; // 1 = droite, -1 = gauche

    public GameObject currentParent;
    private Rigidbody2D currentParentRB;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerID);
    }

    private void Update()
    {
        float moveHorizontal = player.GetAxis("Move Horizontal");
        if (moveHorizontal != 0){
            lastDirection = moveHorizontal > 0 ? 1: -1 ;
        }
        if (currentParent != null)
        {
            moveHorizontal = (moveHorizontal * speed) + currentParentRB.velocity.x;
        }

        rb.velocity = new Vector2(moveHorizontal,rb.velocity.y);

        if (player.GetButtonDown("Jump") && !isJumping){
            rb.AddForce(Vector2.up * jumpForce);
            isJumping = true;
            currentParent = null;
        }

        if(player.GetButtonDown("Dash")){
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

            if (isJumping)
            {
                isJumping = false;
            }
        }
    }
}