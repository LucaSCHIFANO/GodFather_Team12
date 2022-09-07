using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class drag_moove : MonoBehaviour
{
    public GameOver GameOver;

    public projectile projectilePrefab;
    public Transform LaunchOffSet;
 
    private Rigidbody2D rigidbody;

    [SerializeField] private float speed;


    [SerializeField]private int playerID = 0 ;
    [SerializeField]private Player player;

    private void Awake()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }

    void FixedUpdate()
    {
        float moveVertical = player.GetAxis("Vertical");
        float moveHorizontal = player.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(moveHorizontal* speed, moveVertical* speed);


        
    }
    private void Update()
    {
        if (player.GetButtonDown("Shoot"))
        {
            Instantiate(projectilePrefab, LaunchOffSet.position, transform.rotation);
        }
    }
}
