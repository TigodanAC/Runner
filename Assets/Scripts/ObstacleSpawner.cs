using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject[] obstaclePrefabs;

    public float spawnInterval = 1.0f;
    public float spawnDistance = 40f;

    public float minSpacingZ = 6f;
    public float minSpacingRadius = 1f;

    public LayerMask bonusLayer;

    int lanes = 3;
    float laneDistance = 2f;

    private float timer = 0f;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnOne();
            timer = 0f;
        }
    }

    void SpawnOne()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0) return;
        int lane = Random.Range(0, lanes);
        float x = (lane - (lanes - 1) / 2.0f) * laneDistance;
        float y = 0f;
        float z = player.position.z + spawnDistance;

        int prefabIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefab = obstaclePrefabs[prefabIndex];
        Obstacle prefabCfg = prefab.GetComponent<Obstacle>();

        Vector3 offset = Vector3.zero;
        Quaternion rot = Quaternion.identity;
        offset = prefabCfg.spawnOffset;
        rot = Quaternion.Euler(prefabCfg.spawnEuler);

        Vector3 spawnPos = new Vector3(x, y, z) + offset;

        Collider[] hits = Physics.OverlapSphere(spawnPos, minSpacingRadius);
        foreach (var c in hits)
        {
            if (c.gameObject.CompareTag("Obstacle") || c.gameObject.CompareTag("Player") || c.gameObject.CompareTag("Bonus"))
            {
                return;
            }
        }

        GameObject go = Instantiate(prefab, spawnPos, rot, this.transform);

        go.tag = "Obstacle";
    }
}
