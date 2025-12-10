using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject[] bonusPrefabs;
    public float spawnInterval = 4f;
    public float spawnDistance = 75f;
    public float checkRadius = 1.5f;

    float laneDistance = 2f;
    int lanes = 3;

    private float timer = 0f;

    void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnOne();
        }
    }

    void SpawnOne()
    {
        if (bonusPrefabs == null || bonusPrefabs.Length == 0) return;
        
        int lane = Random.Range(0, lanes);
        float x = (lane - (lanes - 1) / 2.0f) * laneDistance;
        float y = 0.6f;
        float z = player.position.z + spawnDistance;
        Vector3 pos = new Vector3(x, y, z);


        Collider[] hits = Physics.OverlapSphere(pos, checkRadius);
        foreach (var c in hits)
        {
            if (c.gameObject.CompareTag("Obstacle") || c.gameObject.CompareTag("Player") || c.gameObject.CompareTag("Bonus"))
            {
                return;
            }
        }

        GameObject prefab = bonusPrefabs[Random.Range(0, bonusPrefabs.Length)];
        Instantiate(prefab, pos, prefab.transform.rotation);
    }
}