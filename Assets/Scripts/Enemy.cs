using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public int coinReward = 10;

    private int currentWaypoint;
    private Pathfinding pathfinding;
    private WaveManager waveManager;

    private void Start()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
        currentWaypoint = 0;
        transform.position = pathfinding.GetWaypointPosition(currentWaypoint);
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void Update()
    {
        Move();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Move()
    {
        Vector3 targetPosition = pathfinding.GetWaypointPosition(currentWaypoint);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition)
        {
            currentWaypoint = pathfinding.GetNextWaypointIndex(currentWaypoint);
            if (currentWaypoint == -1)
            {
                ReachDestination();
            }
        }
    }

    private void Die()
    {
        GameManager.instance.AddScore(coinReward);
        CoinManager.instance.AddCoins(coinReward);
        GameManager.instance.AddHealth(5);
        waveManager.EnemyDied(this.gameObject);
        Destroy(gameObject);
    }

    private void ReachDestination()
    {
        GameManager.instance.ReduceHealth(15);
        waveManager.EnemyDied(this.gameObject);
        Destroy(gameObject);
    }
}