using System.Collections;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyScript enemyScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyScript = GetComponent<EnemyScript>();
    }
    public void Knockback(Transform playerTransform, float knockbackForce, float knockbackTime, float stunTime)
    {
        enemyScript.currentState = EnemyScript.EnemyState.Knockback;
        Vector2 dir = (transform.position - playerTransform.position).normalized;
        rb.linearVelocity = dir * knockbackForce;
        StartCoroutine(StunTimer(knockbackTime, stunTime));

    }

    IEnumerator StunTimer(float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemyScript.currentState = EnemyScript.EnemyState.Idle;
    }
}
