using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float range = 80f;
    public float damage = 34f;
    public float fireRate = 8f; // shots per second
    public Transform muzzle;
    public ParticleSystem muzzleFlash;
    public LayerMask hitMask = ~0;

    float lastShot = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time - lastShot > 1f / fireRate)
        {
            Shoot();
            lastShot = Time.time;
        }
    }

    void Shoot()
    {
        if (muzzleFlash != null) muzzleFlash.Play();

        RaycastHit hit;
        Vector3 origin = muzzle != null ? muzzle.position : transform.position;
        Vector3 dir = transform.forward;
        if (Physics.Raycast(origin, dir, out hit, range, hitMask, QueryTriggerInteraction.Ignore))
        {
            Health h = hit.collider.GetComponent<Health>();
            if (h != null)
            {
                h.TakeDamage(damage);
            }

            // Spawn simple hit effect if desired
            // For now just log
            // Debug.DrawLine(origin, hit.point, Color.red, 1f);
        }
    }
}
