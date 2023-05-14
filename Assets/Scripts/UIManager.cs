using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text playerHealthText;
    public Text scoreText;
    public Text coinText;
    public Text waveText;
    public GameObject gameOverPanel;
    public GameObject gameWonPanel;

    private void Update()
    {
        playerHealthText.text = "Health: " + GameManager.instance.playerHealth;
        scoreText.text = "Score: " + GameManager.instance.score;
        coinText.text = "Coins: " + CoinManager.instance.coins;
        waveText.text = "Waves: " + WaveManager.instance.GetWave() + " / " + WaveManager.instance.GetMaxWave();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}