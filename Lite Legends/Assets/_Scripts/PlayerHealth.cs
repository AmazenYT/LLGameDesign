using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        // Play take hit animation
        if (animator != null)
        {
            animator.SetTrigger("TakeHit");
        }

        Debug.Log($"Player took {amount} damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died.");
        // TODO: Death animation, game over screen, disable input, etc.
    }
}
