using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {
	private const int maxGem = 80;
	private readonly Color MaxHealthColor = Color.green;
	private readonly Color MinHealthColor = Color.red;
	private Animator animator;
	public Image fill;
	public GameObject gameOverMenu;
	public AudioSource gemAudio;
	public int gemCount;
	private int gemDifference;
	public GameObject gemEffectObj;
	public Text gemEffectText;
	public Text gemText;
	public Slider healthBar;
	private bool isGemMax = false;
	private bool isHealthMax = true;
	public int health;
	private bool isInvul = false;

	// Start is called before the first frame update
	private void Start() {
		gemCount = int.Parse(gemText.text);
	}

	// Update is called once per frame
	private void Update() {
		fill.color = Color.Lerp(MinHealthColor, MaxHealthColor, healthBar.value / 3);
		
		if(isGemMax && !isHealthMax)
			gainHealth();

		if (gameObject.transform.position.y < -10 || healthBar.value <= 0)
			Die();
	}

	private void Die() {
		healthBar.value = 0;
		isHealthMax = false;
		gameOverMenu.SetActive(true);
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

	public void LoadPlayer() {
		PlayerData data = DataManagement.LoadData();

		health = data.playerHealth;
		gemCount = data.playerGemCount;
	}
}
