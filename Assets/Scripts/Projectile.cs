using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float speed;
    public Transform target;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}