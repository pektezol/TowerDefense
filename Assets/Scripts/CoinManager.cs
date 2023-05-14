using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coins = 100; // initial amount of coins
    public static CoinManager instance; // singleton instance

    void Awake()
    {
        // Singleton setup
        if (instance != null)
        {
            Debug.LogError("More than one CoinManager in the scene");
        }
        else
        {
            instance = this;
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}