using UnityEngine;

// Attach to the portal object which has a trigger collider in front of it.
// When an object with a Rigidbody enters, it will be moved to the linked portal while
// preserving linear velocity and rotation (optionally).
public class PortalTeleporter : MonoBehaviour
{
    [Tooltip("The linked portal where objects exit")]
    public Transform linkedPortal;

    [Tooltip("Offset to apply outwards from the exit portal to prevent immediate collision")]
    public float exitOffset = 0.6f;

    private void OnTriggerEnter(Collider other)
    {
        if (linkedPortal == null) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        Teleportable t = other.GetComponent<Teleportable>();
        if (t != null && !t.CanTeleport()) return; // respect teleport immunity

        // compute local position relative to this portal
        Transform portalT = this.transform;

        Vector3 localPosition = portalT.InverseTransformPoint(other.transform.position);
        Vector3 localVelocity = portalT.InverseTransformDirection(rb.velocity);
        Quaternion localRotation = Quaternion.Inverse(portalT.rotation) * other.transform.rotation;

        // Mirror across portal forward if portals face each other or have flips; adjust as needed.
        Vector3 newWorldPos = linkedPortal.TransformPoint(localPosition);
        Vector3 newWorldVel = linkedPortal.TransformDirection(localVelocity);
        Quaternion newWorldRot = linkedPortal.rotation * localRotation;

        // Apply an offset along the linked portal forward to avoid re-collision
        Vector3 offset = linkedPortal.forward * exitOffset;

        // If the object has a PlayerController, use dedicated API to set physics safely
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TeleportTo(newWorldPos + offset, newWorldRot, newWorldVel);
        }
        else
        {
            rb.position = newWorldPos + offset;
            rb.rotation = newWorldRot;
            rb.velocity = newWorldVel;
            rb.Sleep(); rb.WakeUp();

            Teleportable tele = other.GetComponent<Teleportable>();
            if (tele != null) tele.OnTeleported();
        }
    }
}
