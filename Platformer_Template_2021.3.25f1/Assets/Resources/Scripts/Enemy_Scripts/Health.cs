using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    private Logger logger;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Log($"Current Health: {currentHealth} {gameObject.name}"); // EX-> Current Health: 90 Enemy (Object being hit so itself)
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Awake()
    {
        logger = FindObjectOfType<Logger>();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void Log(object message)
    {
        if (logger)
            logger.Log(message, this);
    }
}
