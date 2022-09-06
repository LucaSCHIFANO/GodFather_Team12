using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Rigidbody2D  rb;

    [SerializeField] private float jumpForce;
    [SerializeField] private int playerID = 0;
    private bool isJumping = false;

    private void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }


    private void Update()
    {
        if (player.GetButtonDown("Jump") && !isJumping){
            rb.AddForce(Vector2.up * jumpForce);
            isJumping = true;
        }
        
        // if (player.GetButtonDown("Crouch")){
            
        // }
    }

    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Ground" && isJumping){
            isJumping = false;
        }
        Debug.Log("OnCollisionEnter2D");
    }

}
