using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int damage = 30;
    public float knockbackForce = 5f;
    public float stunTime = 1f;
    public float knockbackTime = 0.15f;
    public Transform playerTransform;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);
            collider.GetComponent<EnemyKnockback>().Knockback(playerTransform, knockbackForce, knockbackTime, stunTime);
        }
    }

}
