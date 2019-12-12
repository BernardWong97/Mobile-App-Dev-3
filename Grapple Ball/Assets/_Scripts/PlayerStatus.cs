using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * The main class of the player, controlling the status of the player
 */
public class PlayerStatus : MonoBehaviour {
	// Private variables
	private const int MaxGem = 80;
	private readonly Color _maxHealthColor = Color.green;
	private readonly Color _minHealthColor = Color.red;
	private Animator _animator;
	private int _gemDifference;
	private bool _isGemMax;
	private bool _isHealthMax;

	private bool _isInvul;

	// Public variables
	public Image fill;
	public AudioSource gameOverAudio;
	public GameObject gameOverMenu;
	public AudioSource gemAudio;
	public int gemCount;
	public GameObject gemEffectObj;
	public Text gemEffectText;
	public Text gemText;
	public float health;
	public Slider healthBar;
	public AudioSource hurtAudio;

	// Start is called before the first frame update
	private void Start() {
		if (SceneManager.GetActiveScene().buildIndex > 1) // if scene is not main menu or tutorial, load player data
			LoadPlayer();
	}

	// Update is called once per frame
	private void Update() {
		health = healthBar.value;
		fill.color = Color.Lerp(_minHealthColor, _maxHealthColor, health / 3);

		if (_isGemMax && !_isHealthMax) // gain health when gem is max and health is not
			GainHealth();

		if (gameObject.transform.position.y < -10 || health <= 0) // die when player falls or health depleted
			Die();
	}

	/**
	 * Make player die
	 */
	private void Die() {
		_isHealthMax = false;
		gameOverMenu.SetActive(true);
		gameOverAudio.Play();
		Destroy(gameObject);
	}

	/**
	 * Spend double jump gem
	 */
	public void SpendDouble() {
		SpendGem(20);
	}

	/**
	 * Spend teleport gem
	 */
	public void SpendTeleport() {
		SpendGem(50);
	}

	/**
	 * Regain health to full while spending all gem
	 */
	private void GainHealth() {
		SpendGem(MaxGem);
		healthBar.value = healthBar.maxValue;
		_isGemMax = false;
		_isHealthMax = true;
	}

	/**
	 * Spend number of gem and show effects on UI
	 */
	private void SpendGem(int gem) {
		_isGemMax = false;
		_gemDifference = gem;
		gemCount -= gem;
		gemEffectText.text = "-" + _gemDifference;
		gemText.text = gemCount.ToString();
		SetAnimation("Decrease");
	}

	/**
	 * Set player invulnerable for 1 second
	 */
	private IEnumerator Invul() {
		_isInvul = true;
		yield return new WaitForSeconds(1);
		_isInvul = false;
	}

	private void OnCollisionEnter2D(Collision2D colObj) {
		// if collided object is not fatal or user is invulnerable, ignore
		if (!colObj.gameObject.CompareTag("fatal") || _isInvul) return;

		hurtAudio.Play();
		healthBar.value -= 1;
		_isHealthMax = false;

		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * 800);
		GetComponent<Animator>().SetTrigger("Hurt");
		StartCoroutine(Invul());
	}

	private void OnTriggerEnter2D(Collider2D colObj) {
		if (!colObj.gameObject.CompareTag("gem")) return; // if triggered object is not gem, ignore

		gemAudio.Play();

		if (gemCount < MaxGem) {
			_gemDifference = 5;
			gemCount += 5;
			gemEffectText.text = "+" + _gemDifference;
		}
		else {
			// if gem is max out, stop increase the gem count
			_gemDifference = 0;
			gemCount = 80;
			gemEffectText.text = "MAX";
		}

		if (gemCount == 80)
			_isGemMax = true;

		gemText.text = gemCount.ToString();
		SetAnimation("Increase");
		Destroy(colObj.gameObject);
	}

	/**
	 * Activate animator trigger parameter
	 */
	private void SetAnimation(string stringInput) {
		_animator = gemEffectObj.GetComponent<Animator>();
		_animator.ResetTrigger(stringInput);
		_animator.SetTrigger(stringInput);
	}

	/**
	 * Save player data
	 */
	public void SavePlayer() {
		DataManagement.SaveData(this);
	}

	/**
	 * Load player data
	 */
	private void LoadPlayer() {
		var data = DataManagement.LoadData();

		health = data.playerHealth;
		gemCount = data.playerGemCount;

		healthBar.value = health;
		gemText.text = gemCount.ToString();

		if (health < healthBar.maxValue)
			_isHealthMax = false;

		if (gemCount < MaxGem)
			_isGemMax = false;
	}
}
