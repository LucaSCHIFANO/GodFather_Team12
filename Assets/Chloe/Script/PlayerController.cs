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
    [SerializeField] private float jumpForceWhenDragonGoUp;
    [SerializeField] private float speed;
    private int playerID = 0;
    [SerializeField] private float dashPower;
    [SerializeField]private bool onGround = false;
    [SerializeField]private int jumpNumber = 2;
    private int jumpLeft;

    private int lastDirection = 1; // 1 = droite, -1 = gauche
    private bool isDashing = false;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private float bounceBackForce;
    private bool canDash = true;
    private bool isDamaged = false;

    public GameObject currentParent;
    public drag_move currentDragMove;

    public Mort Mort;

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
        
        if(Mort.dcd == true && player.GetButtonDown("Start"))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("SceneLuca");
        }
        if (Mort.dcd == true && player.GetButtonDown("Select"))
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
            
            if (!onGround)
            {
                jumpLeft = jumpNumber;
                onGround = true;
                if (isDamaged){
                    isDamaged = false;
                }
            }
        }
        //
        // if (col.gameObject.tag == "BreakableWall" && isDashing)
        // {
        //    col.gameObject.GetComponent<HPBlock>().removeHP(10f); 
        // }
    }

    private void OnCollisionStay2D(Collision2D col){
        if (col.gameObject.tag == "Ground")
        {
            if (isDamaged){
                isDamaged = false;
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
        rb.AddForce(new Vector2(-bounceBackForce, bounceBackForce*2));
    }
}