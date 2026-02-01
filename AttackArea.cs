using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int damage = 30;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);
        }
    }

}
