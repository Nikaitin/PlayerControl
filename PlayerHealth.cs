using JetBrains.Annotations;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    private DamageFlash _damageFlash;
    void Start()
    {
        health = maxHealth;
        _damageFlash = GetComponent<DamageFlash>();
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
