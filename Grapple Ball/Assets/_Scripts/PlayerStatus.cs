using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {
	private const int maxGem = 80;
	private readonly Color MaxHealthColor = Color.green;
	private readonly Color MinHealthColor = Color.red;
	private Animator animator;
	public Image fill;
	public GameObject gameOverMenu;
	public AudioSource gemAudio;
	public AudioSource hurtAudio;
	public AudioSource gameOverAudio;
	public int gemCount;
	private int gemDifference;
	public GameObject gemEffectObj;
	public Text gemEffectText;
	public Text gemText;
	public Slider healthBar;
	private bool isGemMax = false;
	private bool isHealthMax;
	public float health;
	private bool isInvul = false;

	// Start is called before the first frame update
	private void Start() {
		if(SceneManager.GetActiveScene().buildIndex > 1)
			LoadPlayer();
	}

	// Update is called once per frame
	private void Update() {
		health = healthBar.value;
		fill.color = Color.Lerp(MinHealthColor, MaxHealthColor, health / 3);

		if(isGemMax && !isHealthMax)
			gainHealth();

		if (gameObject.transform.position.y < -10 || health <= 0)
			Die();
	}

	private void Die() {
		isHealthMax = false;
		gameOverMenu.SetActive(true);
		gameOverAudio.Play();
		Destroy(gameObject);
	}

	public void SpendDouble() {
		spendGem(20);
	}

	public void SpendTeleport() {
		spendGem(50);
	}

	public void gainHealth() {
		spendGem(80);
		healthBar.value = healthBar.maxValue;
		isGemMax = false;
		isHealthMax = true;
	}

	private void spendGem(int gem) {
		isGemMax = false;
		gemDifference = gem;
		gemCount -= gem;
		gemEffectText.text = "-" + gemDifference.ToString();
		gemText.text = gemCount.ToString();
		SetAnimation("Decrease");
	}

	IEnumerator Invul() {
		isInvul = true;
		yield return new WaitForSeconds(1);
		isInvul = false;
	}
	
	private void OnCollisionEnter2D(Collision2D colObj) {
		if (colObj.gameObject.CompareTag("fatal") && !isInvul) {
			hurtAudio.Play();
			healthBar.value -= 1;
			isHealthMax = false;
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * 800);
			GetComponent<Animator>().SetTrigger("Hurt");
			StartCoroutine(Invul());
		}
	}

	private void OnTriggerEnter2D(Collider2D colObj) {
		if (colObj.gameObject.CompareTag("gem")) {
			gemAudio.Play();

			if (gemCount < maxGem) {
				gemDifference = 5;
				gemCount += 5;
				gemEffectText.text = "+" + gemDifference;
			}
			else {
				gemDifference = 0;
				gemCount = 80;
				gemEffectText.text = "MAX";
			}
			
			if(gemCount == 80)
				isGemMax = true;

			gemText.text = gemCount.ToString();
			SetAnimation("Increase");
			Destroy(colObj.gameObject);
		}
	}

	private void SetAnimation(string stringInput) {
		animator = gemEffectObj.GetComponent<Animator>();
		animator.ResetTrigger(stringInput);
		animator.SetTrigger(stringInput);
	}

	public void SavePlayer() {
		DataManagement.SaveData(this);
	}

	private void LoadPlayer() {
		PlayerData data = DataManagement.LoadData();

		health = data.playerHealth;
		gemCount = data.playerGemCount;

		healthBar.value = health;
		gemText.text = gemCount.ToString();

		if (health < healthBar.maxValue)
			isHealthMax = false;

		if (gemCount < maxGem)
			isGemMax = false;
	}
}
