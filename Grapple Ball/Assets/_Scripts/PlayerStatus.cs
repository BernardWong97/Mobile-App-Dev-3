using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public Text gemText;
    public int gemCount;
    private const int maxGem = 80;
    public Text gemEffectText;
    private int gemDifference;
    public AudioSource gemAudio;
    public GameObject gemEffectObj;
    private Animator animator;
    public Slider healthBar;
    public Image fill;
    private Color MinHealthColor = Color.red;
    private Color MaxHealthColor = Color.green;
    public GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        gemCount = int.Parse(gemText.text);
    }

    // Update is called once per frame
    void Update()
    {
        fill.color = Color.Lerp(MinHealthColor, MaxHealthColor, healthBar.value / 3);
        if (gameObject.transform.position.y < -10 || healthBar.value <= 0)
            Die();
    }

    void Die()
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }

    public void SpendDouble()
    {
        gemDifference = -20;
        gemCount -= 20;
        gemEffectText.text = gemDifference.ToString();
        gemText.text = gemCount.ToString();
        setAnimation("Decrease");
    }

    private void OnCollisionEnter2D(Collision2D colObj)
    {
        if (colObj.gameObject.tag == "fatal")
            healthBar.value -= 1;
    }

    private void OnTriggerEnter2D(Collider2D colObj)
    {
        if (colObj.gameObject.tag == "gem")
        {
            gemAudio.Play();
            if (gemCount < maxGem)
            {
                gemDifference = 5;
                gemCount += 5;
                gemEffectText.text = "+" + gemDifference;
            }
            else
            {
                gemDifference = 0;
                gemCount = 80;
                gemEffectText.text = "MAX";
            }
            gemText.text = gemCount.ToString();
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
