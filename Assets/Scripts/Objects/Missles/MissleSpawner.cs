using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawner : MonoBehaviour
{
    public static MissleSpawner Instance;

    [SerializeField]
    bool spawn;

    [SerializeField]
    GameObject misslePrefab;

    [SerializeField]
    float spawnRatePerSecond;

    [SerializeField]
    float spawnAngleArc;

    [SerializeField]
    float spawnRadius;

    float lastSpawnTime = 0.0f;
    // Start is called before the first frame update
    void Awake()
    {
        if (MissleSpawner.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawn) return;
        if (Time.time > lastSpawnTime + (1 / spawnRatePerSecond)) {
            lastSpawnTime = Time.time;
            Spawn();
        }
    }

    void Spawn() {
        float angle = Random.Range(-spawnAngleArc/2.0f, spawnAngleArc/2.0f);
        
        float x = Mathf.Sin(Mathf.Deg2Rad * angle) * spawnRadius;
        float y = Mathf.Cos(Mathf.Deg2Rad * angle) * spawnRadius;

        Vector3 spawnLocation = transform.position + new Vector3(x, y, 0);

        Vector3 vectorToTarget = transform.position - spawnLocation;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * vectorToTarget;
        Quaternion rot = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        GameObject.Instantiate(misslePrefab, spawnLocation, rot, transform);
    }

    [SerializeField]
    bool showDebug = false;
    private void OnDrawGizmos()
    {
        if (!showDebug) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);

        float angle = -spawnAngleArc/2.0f;
        for (int i = 0; i <= 100; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * spawnRadius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * spawnRadius;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(x,y,0));

            angle += ((spawnAngleArc) / 100);
        }
            
    }
}
