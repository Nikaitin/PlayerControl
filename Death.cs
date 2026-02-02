using UnityEngine;

public class Death : MonoBehaviour
{
    public PlayerHealth health;
    public GameObject weapon;
    private GameOverManager gameManager;
    private bool hasDied = false;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameOverManager>();
    }
    void Update()
    {
        if (health.health <= 0 && !hasDied)
        {
            gameManager.TriggerGameOver();
            weapon.SetActive(false);
            gameObject.SetActive(false);
            hasDied = true;
            enabled = false;
        }
    }
    //checking email
}
