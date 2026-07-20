using UnityEngine;

// Simple component that prevents an object from being immediately re-teleported
// after coming out of a portal. The Teleportable is a best-practice to avoid
// infinite portal loops.
public class Teleportable : MonoBehaviour
{
    public float teleportCooldown = 0.2f; // seconds
    private float lastTeleportTime = -10f;

    public void OnTeleported()
    {
        lastTeleportTime = Time.time;
    }

    public bool CanTeleport()
    {
        return Time.time - lastTeleportTime >= teleportCooldown;
    }
}
