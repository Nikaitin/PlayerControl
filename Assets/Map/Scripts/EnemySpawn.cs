using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public int enemyNum;
    public float offset = 10f;

    // Drag your 4 door child objects here in the inspector
    // Order: 0:Up, 1:Down, 2:Left, 3:Right
    public GameObject[] doorObjects;

    // This will keep track of which doors actually exist in this specific room
    // (We don't want to close a door that leads to a wall)
    [HideInInspector] public bool[] hasDoor = new bool[4];
    bool doorsClosed = false;
    bool spawned = false;

    void Update()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (doorsClosed && (enemy == null)) OpenDoors();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        MapSpriteSelector mapper = GetComponent<MapSpriteSelector>();
        if (collision.gameObject.CompareTag("Player") && mapper.type == 0 && !spawned)
        {
            SpawnEnemy(enemyNum);
            CloseDoors();
        }
    }
    void CloseDoors()
    {
        for (int i = 0; i < doorObjects.Length; i++)
        {
            // Only close the door if the map generator said there is a door here
            if (hasDoor[i])
            {
                doorObjects[i].SetActive(true);
            }
        }
        doorsClosed = true;
    }

    public void OpenDoors()
    {
        for (int i = 0; i < doorObjects.Length; i++)
        {
            doorObjects[i].SetActive(false);
        }
        doorsClosed = false;
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
        spawned = true;
    }
}
