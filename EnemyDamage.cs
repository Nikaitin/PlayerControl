using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;
    public float timer = 0f;
    public float damageInterval = 0.5f;

    void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the game object that entered the trigger has the tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            timer = 0f;
            DealDamage(other);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer += Time.deltaTime;
            if (timer >= damageInterval)
            {
                DealDamage(other);
                timer = 0f;
            }
        }
    }

    void DealDamage(Collision2D other)
    {
        PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();
        health.Damage(damage);
    }
}
