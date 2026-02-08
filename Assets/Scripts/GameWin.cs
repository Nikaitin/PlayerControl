using UnityEngine;

public class GameWin : MonoBehaviour
{
    public GameObject weapon;
    private GameOverManager gameManager;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameOverManager>();
        weapon = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.TriggerGameWin();
        weapon.SetActive(false);
    }
}
