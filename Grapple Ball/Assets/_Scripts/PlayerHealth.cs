using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -10)
            Die();
    }

    void Die()
    {
        Debug.Log("Player is dead");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
