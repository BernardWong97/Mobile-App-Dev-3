using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerSpeed = 1;

    public int playerJumpForce = 1250;
    
    private bool facingLeft = false;

    private float moveX;

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        moveX = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump"))
            Jump();
        
        if (moveX < 0.0f && facingLeft == false)
            FlipPlayer();
        else if (moveX > 0.0f && facingLeft == true)
            FlipPlayer();
        
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed,
                                                                    gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpForce);
    }

    void FlipPlayer()
    {
        facingLeft = !facingLeft;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
