using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float runSpeed = 20f;
    [SerializeField] float climbSpeed = 5;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f , 10f);
    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;
    
    Vector2 moveInput;
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    float currentGravity;

    bool isAlive = true;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        currentGravity = myRigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {   if(!isAlive) {return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        QuitGame();
    }
    void OnMove(InputValue value)
    {   
        if(!isAlive) {return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnJump(InputValue value)
    {
       if(!isAlive) {return;}
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}
        if(value.isPressed)
        {
            myRigidbody2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void OnFire(InputValue value)
    {
        if(!isAlive) {return;}
        Instantiate(arrow, bow.position, transform.rotation);
    }
    void Run()
    {
        Vector2 playerVelocoty = new Vector2(moveInput.x * runSpeed, myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = playerVelocoty;
        bool playerHasHoizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHoizontalSpeed);

    }
    void FlipSprite()
    {
        bool playerHasHoizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;

        if(playerHasHoizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }
    }
    void ClimbLadder()
    {
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidbody2D.gravityScale = currentGravity;
            return;
            
        }
       
        myRigidbody2D.gravityScale = 0f;
        Vector2 climbVelocoty = new Vector2(myRigidbody2D.velocity.x, moveInput.y * climbSpeed);
        myRigidbody2D.velocity = climbVelocoty;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }
    void Die()
    {
        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody2D.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    void QuitGame()
    {
        if(Input.GetKey("q"))
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
    }
       
}
