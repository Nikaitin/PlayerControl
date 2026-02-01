using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D body;

    public float timer = 0f;
    public float detectRadius = 10f;
    public float stopDis = 2f;

    // Controls how "smart" the enemy is. 
    // Set to 0 if you want it to track you perfectly every frame.
    public float pathUpdateDelay = 0.2f;
    public float speed = 2f;
    private float playerDis;

    [Header("Lunge Stats")]
    public float windUpTime = 0.5f; // "Stop a bit" duration
    public float lungeSpeed = 10f;  // Burst of speed
    public float lungeDuration = 0.5f; // How long the dash lasts
    public float cooldown = 2f;     // Time between attacks

    private bool isAttacking = false; // Are we currently busy attacking?
    public SpriteRenderer spriteRen; // For visual feedback (Flash Red)
    public Vector2 dir;


    void Update()
    {
        if (player == null) return;
        playerDis = Vector2.Distance(transform.position, player.position);
        if (!isAttacking) Attack();
    }

    private void FacePlayer()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Attack()
    {
        if (playerDis <= detectRadius)
        {
            if (playerDis <= stopDis)
                StartCoroutine(Lunge());
            else
            {

                timer += Time.deltaTime;
                if (timer >= pathUpdateDelay)
                {
                    dir = (player.position - transform.position).normalized;
                    timer = 0f;
                }
                Chase();
            }
        }
        else body.linearVelocity = Vector2.zero;
    }

    private void Chase()
    {
        body.linearVelocity = dir * speed;
        FacePlayer();
    }

    IEnumerator Lunge()
    {
        isAttacking = true;
        //stop before lunging
        body.linearVelocity = Vector2.zero;

        float timeElapsed = 0f;

        Color originalColor = spriteRen.color;
        while (timeElapsed < windUpTime)
        {
            float t = timeElapsed / windUpTime;
            spriteRen.color = Color.Lerp(originalColor, Color.orange, t);
            timeElapsed += Time.deltaTime;
            dir = (player.position - transform.position).normalized;
            FacePlayer();

            yield return null;
        }
        spriteRen.color = Color.orange;

        body.linearVelocity = dir * lungeSpeed;

        spriteRen.color = originalColor;

        // Let the enemy fly forward for the duration of the lunge
        yield return new WaitForSeconds(lungeDuration);

        // PHASE 3: COOLDOWN (Stop sliding)
        body.linearVelocity = Vector2.zero;

        // Wait here so the enemy doesn't attack instantly again
        yield return new WaitForSeconds(cooldown);

        // Reset state so it can chase again
        isAttacking = false;
    }
}
