using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public void TriggerGameOver()
    {
        gameOverUI.SetActive(true);

        //Pause the game physics and time
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // IMPORTANT: Reset time before reloading!
        Time.timeScale = 1f;
        //Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
