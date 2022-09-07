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

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerID);
    }


    private void Update()
    {
        float moveHorizontal = player.GetAxis("Move Horizontal");

        Vector2 movement = new Vector2(moveHorizontal,0);
        rb.velocity = new Vector2(moveHorizontal*speed,rb.velocity.y);

        if (player.GetButtonDown("Jump") && !isJumping){
            rb.AddForce(Vector2.up * jumpForce);
            isJumping = true;
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
        if (col.gameObject.tag == "Ground" && isJumping){
            isJumping = false;
        }
    }
}
