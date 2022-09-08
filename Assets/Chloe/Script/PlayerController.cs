using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Rigidbody2D  rb;
    [SerializeField] private CapsuleCollider2D  col;

    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    private int playerID = 0;
    [SerializeField] private float dashPower;
    [SerializeField]private bool onGround = false;
    private int lastDirection = 1; // 1 = droite, -1 = gauche
    private bool isDashing = false;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private float bounceBackForce;
    private bool canDash = true;
    private bool isDamaged = false;

    public GameObject currentParent;
    private Rigidbody2D currentParentRB;

    public Mort Mort;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerID);

    }

    private void Update()
    {

        if (isDashing || isDamaged){
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
            rb.AddForce(Vector2.up * (jumpForce + currentParentRB.velocity.y));
            onGround = false;
            currentParent = null;
        }

        if(player.GetButtonDown("Dash") && canDash) { 
            StartCoroutine(Dash());
        }
        if(Mort.dcd == true && player.GetButtonDown("Start"))
        {
            SceneManager.LoadScene("SceneLucas");
        }
        if (Mort.dcd == true && player.GetButtonDown("Select"))
        {
            SceneManager.LoadScene("TitleScreen");
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
                if (isDamaged){
                    isDamaged = false;
                }
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

    public void BounceBack(){
        isDamaged = true;
        rb.AddForce(new Vector2(-bounceBackForce, bounceBackForce*2));
    }
}