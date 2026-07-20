using UnityEngine;

// Small helper for booster triggers; place on a trigger volume to launch the player
public class Booster : MonoBehaviour
{
    public Vector3 boostDirection = Vector3.forward;
    public float boostForce = 12f;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Vector3 worldDir = transform.TransformDirection(boostDirection);
            player.ApplyBooster(worldDir, boostForce);
        }
    }
}
