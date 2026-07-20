using UnityEngine;

[RequireComponent(typeof(Health))]
public class SimpleEnemy : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float stopDistance = 2f;
    public float attackRange = 12f;
    public float fireRate = 1f; // shots per second
    public float bulletDamage = 10f;

    Transform player;
    float lastFire;
    Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Start()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;
        Vector3 dir = player.position - transform.position;
        float dist = dir.magnitude;

        if (dist > attackRange)
        {
            // simple chase
            Vector3 move = dir.normalized * moveSpeed * Time.deltaTime;
            transform.position += move;
        }
        else
        {
            // in attack range - look & shoot
            transform.forward = Vector3.Slerp(transform.forward, dir.normalized, Time.deltaTime * 10f);
            if (Time.time - lastFire > 1f / fireRate)
            {
                lastFire = Time.time;
                ShootAtPlayer();
            }
        }
    }

    void ShootAtPlayer()
    {
        if (player == null) return;
        RaycastHit hit;
        Vector3 dir = (player.position + Vector3.up * 0.5f) - transform.position;
        if (Physics.Raycast(transform.position + transform.forward * 0.5f, dir.normalized, out hit, attackRange))
        {
            Health h = hit.collider.GetComponent<Health>();
            if (h != null && hit.collider.CompareTag("Player"))
            {
                h.TakeDamage(bulletDamage);
            }
        }
    }

    // Called by other scripts when enemy takes damage
    public void OnDeath()
    {
        // TODO: spawn VFX / ragdoll / drop
        Destroy(gameObject);
    }
}
