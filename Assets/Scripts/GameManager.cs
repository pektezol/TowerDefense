using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;
    public int playerHealth;

    private void Awake()
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (instance == null)
        {
            instance = this;
            if (uiManager != null)
            {
                // Don't show game win/lose panels at first
                uiManager.gameOverPanel.SetActive(false);
                uiManager.gameWonPanel.SetActive(false);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AllWavesCompleted()
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (playerHealth > 0)
        {
            // Show game won screen
            uiManager.gameWonPanel.SetActive(true);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void AddHealth(int health)
    {
        playerHealth += health;
        if (playerHealth >= 100)
        {
            playerHealth = 100;
        }
    }

    public void ReduceHealth(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

}