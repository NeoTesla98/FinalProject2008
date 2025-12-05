using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Starting health value for the Player
    public int health = 100;
    public Image healthImage; // reference to HealthBar

    // Amount of damage the Player takes when hit
    public int damageAmount = 25;

    // Reference to the Player's SpriteRenderer (used for flashing effect)
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Get the SpriteRenderer component attached to the Player
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateHealthBar(); // update HealthBar at start of game
    }

    // Method to reduce health when damage is taken
    public void TakeDamage()
    {
        health -= damageAmount; // subtract damage amount
        UpdateHealthBar();      // Update HealthBar each frame
        StartCoroutine(BlinkRed()); // briefly flash red

        // If health reaches zero or below, call Die()
        if (health <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthImage != null)
        {
            healthImage.fillAmount = health / 100f;
        }
    }

    // Coroutine to flash the Player red for 0.1 seconds
    private System.Collections.IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    // Reload the scene when the Player dies
    private void Die()
    {
        SceneManager.LoadScene("MainScene");
    }
}

