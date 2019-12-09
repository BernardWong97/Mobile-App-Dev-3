using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class Bullet : MonoBehaviour
{
    public int speed;

    public bool facingLeft = true;

    private Rigidbody2D _bullet;

    private int _xMove;

    // Start is called before the first frame update
    void Start()
    {
        if (facingLeft)
            speed *= -1;
        _bullet = gameObject.GetComponent<Rigidbody2D>();
        _bullet.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (facingLeft)
            _xMove = -1;
        else
            _xMove = 1;
            
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(_xMove, 0));

        if (hit.distance < 0.7f)
        {
            Destroy(gameObject);
            Debug.Log("HIT " + hit.collider.name);
        }
            
        
    }
}
