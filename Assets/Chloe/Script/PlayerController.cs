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
    [SerializeField] private int speed;
    [SerializeField] private int playerID = 0;
    [SerializeField]private bool isJumping = false;
    [SerializeField] private int health = 5;

    public GameObject currentParent;
    private Rigidbody2D currentParentRB;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerID);
    }


    private void Update()
    {
        float moveHorizontal = player.GetAxis("Move Horizontal");

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
        
        // if (player.GetButtonDown("Crouch")){
        //     col.offset = new Vector2(0,-0.25f);
        //     col.size = new Vector2(1,0.5f);
        //     GetComponent<SpriteRenderer>().sprite = spriteCrouch;
        // }

        // if (player.GetButtonUp("Crouch")){
        //     col.offset = new Vector2(0,0f);
        //     col.size = new Vector2(1,1f);
        //     GetComponent<SpriteRenderer>().sprite = sprite;
        // }
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
