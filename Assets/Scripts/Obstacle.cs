using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damage = 1;
    public Vector3 spawnOffset = Vector3.zero;
    public Vector3 spawnEuler = Vector3.zero;

    float despawnZOffset = -15f;
    bool wasHit = false;

    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        if (transform.position.z < player.transform.position.z + despawnZOffset)
        {
            Destroy(gameObject);
        }
    }

    public bool Hit()
    {
        if (wasHit) return false;
        wasHit = true;

        Destroy(gameObject);
        return true;
    }
}
