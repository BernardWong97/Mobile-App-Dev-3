using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int enemySpeed = 5;

    public bool facingLeft = false;

    public bool isTurret = false;

    public GameObject bulletObj;
    
    private int _xMove;
    
    // Start is called before the first frame update
    void Start()
    {
        if(isTurret)
            InvokeRepeating("Shoot", 0.2f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTurret)
        {
            if (facingLeft)
                _xMove = -1;
            else
                _xMove = 1;
        
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(_xMove, 0));
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_xMove, 0) * enemySpeed;

            if (hit.distance < 0.7f)
                Flip();
        }
    }

    void Flip()
    {
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

        if (facingLeft)
            facingLeft = false;
        else
            facingLeft = true;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletObj);
        bullet.transform.position = gameObject.transform.position;
    }
}
