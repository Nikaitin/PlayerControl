using JetBrains.Annotations;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    private DamageFlash _damageFlash;
    void Start()
    {
        health = maxHealth;
        _damageFlash = GetComponent<DamageFlash>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
    public void Damage(int damage)
    {
        health -= damage;
        if (_damageFlash != null)
        {
            _damageFlash.Flash();
        }
    }
}
