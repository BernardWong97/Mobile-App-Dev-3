using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public Text gemText;
    private int gemCount;
    public Text gemEffectText;
    private int gemDifference;
    public AudioSource gemAudio;
    public GameObject gemEffectObj;
    private Animator animator;
    private string currentAnimation = null;
    
    // Start is called before the first frame update
    void Start()
    {
        gemCount = int.Parse(gemText.text);
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

    private void OnCollisionEnter2D(Collision2D colObj)
    {
        if(colObj.gameObject.tag == "fatal")
            Die();
    }

    private void OnTriggerEnter2D(Collider2D colObj)
    {
        if (colObj.gameObject.tag == "gem")
        {
            gemAudio.Play();
            gemDifference = 5;
            gemCount += 5;
            gemText.text = gemCount.ToString();
            gemEffectText.text = "+" + gemDifference;
            setAnimation("Increase");
            Destroy(colObj.gameObject);
        }
            
    }
    
    void setAnimation(string stringInput){
        animator = gemEffectObj.GetComponent<Animator>();
        animator.ResetTrigger(stringInput);
        animator.SetTrigger(stringInput);
    }
}
