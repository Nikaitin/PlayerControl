using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public int enemyNum;
    public float offset = 10f;


    void OnTriggerEnter2D(Collider2D collision)
    {
        MapSpriteSelector mapper = GetComponent<MapSpriteSelector>();
        if (collision.gameObject.CompareTag("Player") && mapper.type == 0)
        {
            SpawnEnemy(enemyNum);
        }
    }

    void SpawnEnemy(int number)
    {
        float spawnX = transform.position.x;
        float spawnY = transform.position.y;
        while (number != 0)
        {
            Vector2 randomPos = new Vector2(spawnX + Random.Range(offset, -offset), spawnY + Random.Range(offset, -offset));
            Instantiate(enemy, randomPos, Quaternion.identity);
            number--;
        }
    }
}
