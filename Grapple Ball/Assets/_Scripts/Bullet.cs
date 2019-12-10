using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed;

    public bool facingLeft = true;

    private Rigidbody2D _bullet;
    

    // Start is called before the first frame update
    void Start()
    {
        if (facingLeft)
            speed *= -1;
        _bullet = gameObject.GetComponent<Rigidbody2D>();
        _bullet.velocity = new Vector2(speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D colObj)
    {
        Destroy(gameObject);
    }
}
