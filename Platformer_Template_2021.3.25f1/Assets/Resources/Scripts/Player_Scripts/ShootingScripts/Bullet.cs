using UnityEngine;

/// <summary>
///
/// License:
/// Copyrighted to Brian "VerzatileDev" Lätt ©2024.
/// Do not copy, modify, or redistribute this code without prior consent.
/// All rights reserved.
///
/// </summary>
/// <remarks>
/// Its seperate entity when triggered adds speed and goes in that direction until the end of its lifetime. <br></br>
/// Unless it hits an object <br></br>
/// If it hits an object with health, it will deal damage to it. <br></br>
/// </remarks>
public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRigidbody = null;
    public float BulletSpeed { get; set; } = 20f;
    [SerializeField] private float bulletLifetime = 5f; // Lifetime of the bullet in seconds Currently Defined Only here.
    private int damage = 2;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        if (bulletRigidbody == null)
        {
            Debug.LogError("Bullet Rigidbody component not found on object!");
            return;
        }
    }

    private void Start()
    {
        MoveBullet();
        DestroyBulletAfterLifetime();
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void MoveBullet()
    {
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = transform.right * BulletSpeed;
        }
        else
        {
            Debug.LogError("Bullet Rigidbody component not found on object!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the "Enemy" tag (or the appropriate tag you set for objects with health)
        if (other.CompareTag("Enemy"))
        {
            // Get the Health component from the collided object
            Health healthComponent = other.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(damage);
            }
        }
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void DestroyBulletAfterLifetime()
    {
        Invoke("DestroyBullet", bulletLifetime);
    }
}
