using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerSpeed = 1;

    public int playerJumpForce = 1250;

    public Animator animator;

    public AudioSource jumpAudio;

    public AudioSource gemAudio;

    public AudioSource itemAudio;

    private bool facingLeft = false;

    private float moveX;

    private bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        moveX = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && isGrounded)
            Jump();
        
        if (moveX < 0.0f && facingLeft == false)
            FlipPlayer();
        else if (moveX > 0.0f && facingLeft == true)
            FlipPlayer();
        
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed,
                                                                    gameObject.GetComponent<Rigidbody2D>().velocity.y);
        
        animator.SetFloat("Speed", Mathf.Abs(moveX));
    }

    void Jump()
    {
        jumpAudio.Play();
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpForce);
        isGrounded = false;
    }

    void FlipPlayer()
    {
        facingLeft = !facingLeft;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D (Collision2D colObj)
    {
        if (colObj.gameObject.tag == "ground")
            isGrounded = true;
    }
}
