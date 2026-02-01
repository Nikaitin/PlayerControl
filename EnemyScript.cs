using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public float timer = 0f;
    public float attackTime = 2f;
    public float detectRadius = 10f;
    public float speed = 2f;
    private float playerDis;
    private Vector2 playerPos;

    // Update is called once per frame
    void Update()
    {
        playerDis = Vector2.Distance(transform.position, player.position);
        Attack();
    }

    private void Attack()
    {
        if (playerDis <= detectRadius)
        {
            timer += Time.deltaTime;
            if (timer >= attackTime)
            {
                playerPos = player.position;
                timer = 0f;
            }
            Chase();
        }
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
    }
}
