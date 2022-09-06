using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class drag_moove : MonoBehaviour
{
    public projectile Projectile;

    private Rigidbody2D rigidbody;

    [SerializeField] private float speed;


    [SerializeField]private int playerID = 0 ;
    [SerializeField]private Player player;


     void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveVertical = player.GetAxis("Vertical");

        rigidbody.velocity = new Vector2(0, moveVertical);
    }
}
