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

    private void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }


    private void FixedUpdate()
    {
        if (player.GetButtonDown("Jump")){
            rb.AddForce(Vector3.up * jumpForce);
        }
        
        // if (player.GetButtonDown("Crouch")){
            
        // }
    }
}
