using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Rigidbody2D  rb;
    [SerializeField] private CapsuleCollider2D  col;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpForceWhenDragonGoUp;
    [SerializeField] private float speed;
    private int playerID = 0;
    [SerializeField] private float dashPower;
    [SerializeField]public bool onGround = false;
    [SerializeField]private int jumpNumber = 2;
    private int jumpLeft;

    private int lastDirection = 1; // 1 = droite, -1 = gauche
    private bool isDashing = false;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private float bounceBackForceleft;
    [SerializeField] private float bounceBackForceup;
    [SerializeField] private float bounceBackTime = 1f;
    private bool canDash = true;
    private bool isDamaged = false;

    public GameObject currentParent;
    public drag_move currentDragMove;

    public GameOverScreen gameOver;

    private Animator anim;
    private SpriteRenderer sr;

    [SerializeField] private float normalGrav;
    [SerializeField] private float jumpGrav;
    [SerializeField] private BoxCollider2D LeftCC;
    [SerializeField] private BoxCollider2D RightCC;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerID);
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        jumpLeft = jumpNumber;
    }

    private void Update()
    {
        if (isDashing || isDamaged){
            return;
        }
        float moveHorizontal = player.GetAxis("Move Horizontal");
        anim.SetFloat("Move", Mathf.Abs(moveHorizontal));
        if (moveHorizontal > 0.2f) sr.flipX = false;
        else if (moveHorizontal < -0.2f) sr.flipX = true;
        
        if (moveHorizontal != 0){
            lastDirection = moveHorizontal > 0 ? 1: -1 ;
        }
        if (currentParent != null)
        {
            moveHorizontal = (moveHorizontal * speed);
        } else{
            moveHorizontal = moveHorizontal * speed;
        } 
        rb.velocity = new Vector2(moveHorizontal,rb.velocity.y);

        if (player.GetButtonDown("Jump") && jumpLeft > 0)
        {
            jumpLeft -= 1;
            rb.gravityScale = jumpGrav;
            
            if (!currentDragMove.retour) rb.AddForce(Vector2.up * (jumpForceWhenDragonGoUp));
            else rb.AddForce(Vector2.up * (jumpForce));
            
            anim.Play("Human_Jump");
            anim.SetBool("IsJumping", true);
            
            onGround = false;
            currentParent = null;
        }

        if(player.GetButtonDown("Dash") && canDash) { 
            StartCoroutine(Dash());
        }
        
        if (gameOver.Chrono == true && player.GetButtonDown("Start"))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("SceneLuca");
        }
        if (gameOver.Chrono == true && player.GetButtonDown("Select"))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("TitleScreen");
        }
        
        if(rb.velocity.y < 0 && !onGround) anim.Play("Human_Fall");
    }

    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Ground")
        {
            /*if (rb.velocity.y <= 0)
            {*/
                currentParent = col.gameObject;
                anim.SetBool("IsJumping", false);
                anim.Play("Human_Idle");
            //}

            if (rb.velocity.y <= 0) rb.gravityScale = normalGrav;
            
            if (!onGround && col.transform.position.y < transform.position.y)
            {
                jumpLeft = jumpNumber;
                onGround = true;
            }
        }
    }

    private IEnumerator Dash(){
        anim.Play("Human_Dash");
        canDash = false;
        isDashing = true;
        
        if(lastDirection > 0) RightCC.gameObject.SetActive(true);
        else LeftCC.gameObject.SetActive(true);
        
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower * lastDirection, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        anim.Play("Human_Fall");
        
        RightCC.gameObject.SetActive(false);
        LeftCC.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void BounceBack(){
        isDamaged = true;
        //rb.gravityScale = normalGrav;
        //rb.velocity = new Vector2(-bounceBackForce * 2, bounceBackForce);
        //rb.velocity = new Vector2(-bounceBackForceleft, bounceBackForceup);
        rb.velocity = Vector2.zero; 
        rb.AddForce(new Vector2(-bounceBackForceleft,bounceBackForceup), ForceMode2D.Impulse);
        //rb.AddForce(Vector2.up * bounceBackForce);
        StartCoroutine(EndIsDamaged());
        StartCoroutine(HitStun());
    }

    private IEnumerator EndIsDamaged()
    {
        yield return new WaitForSeconds(0.45f);
        rb.velocity = Vector2.zero;
    }
    private IEnumerator HitStun()
    {
        yield return new WaitForSeconds(1.1f);
        rb.velocity = Vector2.zero;
        isDamaged = false;
    }
}