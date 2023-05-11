using UnityEngine;
using UnityEngine.Tilemaps;

public class Tower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject towerPrefab;
    public Tilemap towerPlacementTilemap;
    public float range;
    public float damage;
    public float attackSpeed;
    public int towerCost = 100;

    private float lastAttackTime;
    private Transform target;

    private void Update()
    {
        FindTarget();
        if (target != null && Time.time > lastAttackTime + 1f / attackSpeed)
        {
            Shoot(target);
            lastAttackTime = Time.time;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = towerPlacementTilemap.WorldToCell(worldPos);

            if (towerPlacementTilemap.HasTile(cellPos))
            {
                // Check if there is already a turret at this position
                Collider2D hitCollider = Physics2D.OverlapCircle(towerPlacementTilemap.GetCellCenterWorld(cellPos), 0.1f);

                if (hitCollider == null)
                {
                    // Only place a turret if there isn't one already and the player has enough coins
                    if (CoinManager.instance.SpendCoins(towerCost))
                    {
                        Instantiate(towerPrefab, towerPlacementTilemap.GetCellCenterWorld(cellPos), Quaternion.identity);
                    }
                }
            }
        }
    }

    private void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < range && distanceToEnemy < minDistance)
            {
                target = enemy.transform;
                minDistance = distanceToEnemy;
            }
        }
    }

    private void Shoot(Transform target)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = newProjectile.GetComponent<Projectile>();
        projectile.damage = damage;
        projectile.speed = 5f; // Adjust as needed
        projectile.target = target;
    }
}